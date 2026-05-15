using Microsoft.AspNetCore.Mvc;
using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Facades;

namespace ParkingManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSessionsController : ControllerBase
    {
        private IParkingSessionFacade _parkingSessionFacade;

        public ParkingSessionsController(IParkingSessionFacade parkingSessionFacade)
        {
            _parkingSessionFacade = parkingSessionFacade;
        }

        [HttpPost]
        public async Task<ActionResult> Entry([FromBody] EntryCommand command)
        {
            try
            {
                await _parkingSessionFacade.Entry(command);
                return NoContent();
            } catch(Exception e)
            {
                return StatusCode(500, $"Erro interno: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Exit([FromRoute] Guid id)
        {
            try
            {
                await _parkingSessionFacade.Exit(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro interno: {e.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSessionDto>>> Get()
        {
            var parkingSessions = await _parkingSessionFacade.GetAll();
            return Ok(parkingSessions);
        }

        [HttpGet("{licensePlate}")]
        public async Task<ActionResult<ParkingSessionDto>> Get([FromRoute] string licensePlate)
        {
            var parkingSession = await _parkingSessionFacade.GetByLicensePlate(licensePlate);

            if (parkingSession.Id == Guid.Empty)
                return NotFound();

            return Ok(parkingSession);
        }
    }
}

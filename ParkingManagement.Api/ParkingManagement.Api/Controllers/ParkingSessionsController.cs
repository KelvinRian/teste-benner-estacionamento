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
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Exit([FromRoute] Guid id)
        {
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSessionDto>>> Get()
        {
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSessionDto>> Get([FromRoute] Guid id)
        {
            return NoContent();
        }
    }
}

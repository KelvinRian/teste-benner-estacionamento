using Microsoft.AspNetCore.Mvc;
using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Facades;

namespace ParkingManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly IPriceFacade _priceFacade;

        public PricesController(IPriceFacade priceFacade)
        {
            _priceFacade = priceFacade;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePriceCommand command)
        {
            try
            {
                await _priceFacade.Create(command);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Erro interno: {e.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceDto>>> Get()
        {
            var prices = await _priceFacade.GetAll();
            return Ok(prices);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _priceFacade.Delete(id);
                return NoContent();
            } catch (Exception e)
            {
                return StatusCode(500, $"Erro interno: {e.Message}");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> Create()
        {
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceDto>>> Get()
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            return NoContent();
        }
    }
}

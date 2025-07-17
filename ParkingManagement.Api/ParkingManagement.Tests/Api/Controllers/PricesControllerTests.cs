using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ParkingManagement.Api.Controllers;
using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Facades;

namespace ParkingManagement.Tests.Api.Controllers
{
    public class PricesControllerTests
    {
        private IPriceFacade _priceFacade;
        private readonly PricesController _controller;

        public PricesControllerTests()
        {
            _priceFacade = Substitute.For<IPriceFacade>();
            _controller = new PricesController(_priceFacade);
        }

        [Fact]
        public async Task shouldCreate()
        {
            var command = new CreatePriceCommand();

            var result = await _controller.Create(command);

            await _priceFacade.Received(1).Create(command);
        }

        [Fact]
        public async Task shouldGet()
        {
            var dtos = new List<PriceDto>();
            _priceFacade.GetAll().Returns(dtos);

            var result = await _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<List<PriceDto>>(okResult.Value);
            Assert.Equal(dtos, value);
        }

        [Fact]
        public async Task shouldDelete()
        {
            var id = Guid.NewGuid();

            await _controller.Delete(id);

            await _priceFacade.Received(1).Delete(id);
        }
    }
}

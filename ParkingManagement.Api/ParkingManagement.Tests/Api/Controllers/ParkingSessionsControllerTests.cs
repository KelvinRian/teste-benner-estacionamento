using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ParkingManagement.Api.Controllers;
using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Facades;

namespace ParkingManagement.Tests.Api.Controllers
{
    public class ParkingSessionsControllerTests
    {
        private IParkingSessionFacade _sessionFacade;
        private readonly ParkingSessionsController _controller;

        public ParkingSessionsControllerTests()
        {
            _sessionFacade = Substitute.For<IParkingSessionFacade>();
            _controller = new ParkingSessionsController(_sessionFacade);
        }

        [Fact]
        public async Task shouldEntry()
        {
            var command = new EntryCommand();

            var result = await _controller.Entry(command);

            await _sessionFacade.Received(1).Entry(command);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task shouldExit()
        {
            var id = Guid.NewGuid();

            var result = await _controller.Exit(id);

            await _sessionFacade.Received(1).Exit(id);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task shouldGet()
        {
            var dtos = new List<ParkingSessionDto>();
            _sessionFacade.GetAll().Returns(dtos);

            var result = await _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<List<ParkingSessionDto>>(okResult.Value);
            Assert.Equal(dtos, value);
        }

        [Fact]
        public async Task shouldGetByLicensePlate()
        {
            var dto = new ParkingSessionDto()
            {
                Id = Guid.NewGuid(),
            };

            var licensePlate = "aaa-1234";
            _sessionFacade.GetByLicensePlate(licensePlate).Returns(dto);

            var result = await _controller.Get(licensePlate);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<ParkingSessionDto>(okResult.Value);
            Assert.Equal(dto, value);
        }

        [Fact]
        public async Task shouldNotGetByLicensePlate()
        {
            var emptyDto = new ParkingSessionDto();

            var licensePlate = "aaa-1234";
            _sessionFacade.GetByLicensePlate(licensePlate).Returns(emptyDto);

            var result = await _controller.Get(licensePlate);

            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}

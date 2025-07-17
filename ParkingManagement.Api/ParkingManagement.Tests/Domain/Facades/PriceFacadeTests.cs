using NSubstitute;
using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.Facades;
using ParkingManagement.Domain.RepositoryInterfaces;

namespace ParkingManagement.Tests.Domain.Facades
{
    public class PriceFacadeTests
    {
        private readonly IPriceRepository _repository;
        private readonly IPriceFacade _facade;

        public PriceFacadeTests()
        {
            _repository = Substitute.For<IPriceRepository>();
            _facade = new PriceFacade(_repository);
        }

        [Fact]
        public async Task shouldCreate()
        {
            // Arrange
            var command = new CreatePriceCommand()
            {
                BaseValue = 1,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow,
                EffectivePeriodEnd = DateTime.UtcNow.AddDays(1)
            };

            // Act
            await _facade.Create(command);

            // Assert
            await _repository.Received(1)
                .Create(Arg.Is<Price>(x => x.BaseValue == command.BaseValue 
                                && x.ExtraTimeValue == command.ExtraTimeValue
                                && x.EffectivePeriodStart == command.EffectivePeriodStart
                                && x.EffectivePeriodEnd == command.EffectivePeriodEnd));
        }

        [Fact]
        public async Task shouldDelete()
        {
            // Arrange
            var command = new CreatePriceCommand();
            var price = new Price(command);

            var id = Guid.NewGuid();

            _repository.GetById(id).Returns(price);

            // Act
            await _facade.Delete(id);

            // Assert
            await _repository.Received(1)
                .Delete(id);
        }

        [Fact]
        public async Task shouldGetAll()
        {
            // Arrange
            var command = new CreatePriceCommand()
            {
                BaseValue = 1,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow,
                EffectivePeriodEnd = DateTime.UtcNow.AddDays(1)
            };
            var price1 = new Price(command);
            var price2 = new Price(command);

            _repository.GetAll().Returns(new List<Price>() { price1, price2 });

            // Act
           var result = await _facade.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, x => x.BaseValue == command.BaseValue
                            && x.ExtraTimeValue == command.ExtraTimeValue
                            && x.EffectivePeriodStart == command.EffectivePeriodStart
                            && x.EffectivePeriodEnd == command.EffectivePeriodEnd);
        }
    }
}

using EntityFrameworkCore.Testing.Moq;
using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Infrastructure.Context;
using ParkingManagement.Infrastructure.Repositories;

namespace ParkingManagement.Tests.Infrastructure.Repositories
{
    public class PriceRepositoryTests
    {
        [Fact]
        public async Task shouldCreate()
        {
            // Arrange
            var mockContext = Create.MockedDbContextFor<ParkingManagementDbContext>();

            var repository = new PriceRepository(mockContext);

            var command = new CreatePriceCommand()
            {
                BaseValue = 2,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow,
                EffectivePeriodEnd = DateTime.UtcNow
            };

            var price = new Price(command);

            // Act
            await repository.Create(price);

            // Assert
            var createdPrice = mockContext.Prices.Local.FirstOrDefault();
            Assert.NotNull(createdPrice);
            Assert.Equal(price, createdPrice);
        }

        [Fact]
        public async Task shouldDelete()
        {
            // Arrange
            var mockContext = Create.MockedDbContextFor<ParkingManagementDbContext>();

            var repository = new PriceRepository(mockContext);

            var command = new CreatePriceCommand()
            {
                BaseValue = 2,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow,
                EffectivePeriodEnd = DateTime.UtcNow
            };

            var price = new Price(command);

            await mockContext.AddAsync(price);
            await mockContext.SaveChangesAsync();

            // Act
            await repository.Delete(price.Id);

            // Assert
            var result = mockContext.Prices.Local.FirstOrDefault();
            Assert.Null(result);
        }

        [Fact]
        public async Task shouldGetAll()
        {
            // Arrange
            var mockContext = Create.MockedDbContextFor<ParkingManagementDbContext>();

            var command = new CreatePriceCommand()
            {
                BaseValue = 2,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow,
                EffectivePeriodEnd = DateTime.UtcNow
            };

            var price = new Price(command);

            await mockContext.Prices.AddAsync(price);
            await mockContext.SaveChangesAsync();

            var repository = new PriceRepository(mockContext);

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.Single(result);
            Assert.Equal(price, result.First());
        }

        [Fact]
        public async Task shouldGetLatestWhenThereIsNoApplicable()
        {
            // Arrange
            var mockContext = Create.MockedDbContextFor<ParkingManagementDbContext>();

            var commandTenDaysAgo = new CreatePriceCommand()
            {
                BaseValue = 2,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow.AddDays(-10),
                EffectivePeriodEnd = DateTime.UtcNow.AddDays(-10)
            };
            var priceTenDaysAgo = new Price(commandTenDaysAgo);

            var commandFiveDaysAgo = new CreatePriceCommand()
            {
                BaseValue = 2,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow.AddDays(-5),
                EffectivePeriodEnd = DateTime.UtcNow.AddDays(-5)
            };
            var priceFiveDaysAgo = new Price(commandFiveDaysAgo);

            var commandElevenDaysAgo = new CreatePriceCommand()
            {
                BaseValue = 2,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow.AddDays(-11),
                EffectivePeriodEnd = DateTime.UtcNow.AddDays(-11)
            };
            var priceElevenDaysAgo = new Price(commandElevenDaysAgo);

            await mockContext.Prices.AddRangeAsync(priceTenDaysAgo, priceFiveDaysAgo, priceElevenDaysAgo);
            await mockContext.SaveChangesAsync();

            var repository = new PriceRepository(mockContext);

            // Act
            var parkingSessionEntryDate = DateTime.UtcNow.AddDays(-1);
            var result = await repository.GetLatestApplicableFor(parkingSessionEntryDate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(priceFiveDaysAgo, result);
        }

        [Fact]
        public async Task shouldGetLatestApplicableFor()
        {
            // Arrange
            var mockContext = Create.MockedDbContextFor<ParkingManagementDbContext>();

            var commandTenToEightDaysAgo = new CreatePriceCommand()
            {
                BaseValue = 2,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow.AddDays(-10),
                EffectivePeriodEnd = DateTime.UtcNow.AddDays(-8)
            };
            var oldestPriceTenToEightDaysAgo = new Price(commandTenToEightDaysAgo);

            var commandFiveDaysAgo = new CreatePriceCommand()
            {
                BaseValue = 2,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow.AddDays(-5),
                EffectivePeriodEnd = DateTime.UtcNow.AddDays(-5)
            };
            var priceFiveDaysAgo = new Price(commandTenToEightDaysAgo);

            var newestPriceTenToEightDaysAgo = new Price(commandTenToEightDaysAgo);

            await mockContext.Prices.AddRangeAsync(oldestPriceTenToEightDaysAgo, priceFiveDaysAgo, newestPriceTenToEightDaysAgo);
            await mockContext.SaveChangesAsync();

            var repository = new PriceRepository(mockContext);

            // Act
            var parkingSessionEntryDate = DateTime.UtcNow.AddDays(-9);
            var result = await repository.GetLatestApplicableFor(parkingSessionEntryDate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newestPriceTenToEightDaysAgo, result);
        }

        [Fact]
        public async Task shouldHaveParkingSession()
        {
            // Arrange
            var mockContext = Create.MockedDbContextFor<ParkingManagementDbContext>();

            var priceCommand = new CreatePriceCommand()
            {
                BaseValue = 2,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow,
                EffectivePeriodEnd = DateTime.UtcNow
            };

            var price = new Price(priceCommand);

            await mockContext.AddAsync(price);
            await mockContext.SaveChangesAsync();

            var parkingSessionCommand = new EntryCommand()
            {
                LicensePlate = "aaa-1234"
            };

            var parkingSession = new ParkingSession(parkingSessionCommand, price.Id);

            await mockContext.AddAsync(parkingSession);
            await mockContext.SaveChangesAsync();

            var repository = new PriceRepository(mockContext);

            // Act
            var result = await repository.HasAnyParkingSession(price.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task shouldNotHaveParkingSession()
        {
            // Arrange
            var mockContext = Create.MockedDbContextFor<ParkingManagementDbContext>();

            var priceCommand = new CreatePriceCommand()
            {
                BaseValue = 2,
                ExtraTimeValue = 1,
                EffectivePeriodStart = DateTime.UtcNow,
                EffectivePeriodEnd = DateTime.UtcNow
            };

            var price = new Price(priceCommand);

            await mockContext.AddAsync(price);
            await mockContext.SaveChangesAsync();

            var repository = new PriceRepository(mockContext);

            // Act
            var result = await repository.HasAnyParkingSession(price.Id);

            // Assert
            Assert.False(result);
        }
    }
}

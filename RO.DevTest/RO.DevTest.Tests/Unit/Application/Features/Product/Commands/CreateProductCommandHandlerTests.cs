using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Moq;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using Xunit;

namespace RO.DevTest.Tests.Unit.Application.Features.Product.Commands;

public class CreateProductCommandHandlerTests {
    private readonly Faker _faker = new Faker();

    [Fact]
    public async Task Handle_ShouldCreateProduct() {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var handler = new CreateProductCommandHandler(mockRepo.Object);

        var command = new CreateProductCommand {
            Name = _faker.Commerce.ProductName(),
            Description = _faker.Commerce.ProductDescription(),
            Price = decimal.Parse(_faker.Commerce.Price()),
            StockQuantity = _faker.Random.Int(1, 100)
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.Description.Should().Be(command.Description);
        result.Price.Should().Be(command.Price);
        result.StockQuantity.Should().Be(command.StockQuantity);
        mockRepo.Verify();
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Moq;
using RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using Xunit;

namespace RO.DevTest.Tests.Unit.Application.Features.Sale.Commands;

public class CreateSaleCommandHandlerTests {
    private readonly Faker _faker = new Faker();

    [Fact]
    public async Task Handle_ShouldCreateSale() {
        // Arrange
        var mockSaleRepo = new Mock<ISaleRepository>();
        var mockProductRepo = new Mock<IProductRepository>();

        mockSaleRepo.Setup(r => r.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        mockProductRepo.Setup(r => r.Get(It.IsAny<Func<Product, bool>>()))
            .Returns(new Product {
                Id = Guid.NewGuid(),
                Name = _faker.Commerce.ProductName(),
                Price = decimal.Parse(_faker.Commerce.Price()),
                StockQuantity = 100
            });

        var handler = new CreateSaleCommandHandler(mockSaleRepo.Object, mockProductRepo.Object);

        var command = new CreateSaleCommand {
            ClientId = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            Items = new List<SaleItemDto> {
                new SaleItemDto {
                    ProductId = Guid.NewGuid(),
                    Quantity = 2
                }
            }
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.ClientId.Should().Be(command.ClientId);
        result.Items.Should().HaveCount(1);
        mockSaleRepo.Verify();
        mockProductRepo.Verify();
    }
}

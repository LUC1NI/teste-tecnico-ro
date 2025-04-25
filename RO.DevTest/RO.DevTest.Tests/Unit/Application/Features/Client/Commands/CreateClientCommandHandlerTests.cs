using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Moq;
using RO.DevTest.Application.Features.Client.Commands.CreateClientCommand;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities.Client;
using Xunit;

namespace RO.DevTest.Tests.Unit.Application.Features.Client.Commands;

public class CreateClientCommandHandlerTests {
    private readonly Faker _faker = new Faker();

    [Fact]
    public async Task Handle_ShouldCreateClient() {
        // Arrange
        var mockRepo = new Mock<IClientRepository>();
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var handler = new CreateClientCommandHandler(mockRepo.Object);

        var command = new CreateClientCommand {
            Name = _faker.Person.FullName,
            Email = _faker.Internet.Email(),
            PhoneNumber = _faker.Phone.PhoneNumber()
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.Email.Should().Be(command.Email);
        result.PhoneNumber.Should().Be(command.PhoneNumber);
        mockRepo.Verify();
    }
}

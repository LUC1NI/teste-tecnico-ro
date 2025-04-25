using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Client.Commands.CreateClientCommand;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CreateClientResult> {
    private readonly IClientRepository _clientRepository;

    public CreateClientCommandHandler(IClientRepository clientRepository) {
        _clientRepository = clientRepository;
    }

    public async Task<CreateClientResult> Handle(CreateClientCommand request, CancellationToken cancellationToken) {
        var entity = new RO.DevTest.Domain.Entities.Client {
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        await _clientRepository.CreateAsync(entity, cancellationToken);

        return new CreateClientResult {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber
        };
    }
}

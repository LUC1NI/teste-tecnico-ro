using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Client.Commands.UpdateClientCommand;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, UpdateClientResult> {
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IClientRepository clientRepository) {
        _clientRepository = clientRepository;
    }

    public async Task<UpdateClientResult> Handle(UpdateClientCommand request, CancellationToken cancellationToken) {
        var entity = _clientRepository.Get(c => c.Id == request.Id);
        if (entity == null) {
            throw new KeyNotFoundException("Client not found");
        }

        entity.Name = request.Name;
        entity.Email = request.Email;
        entity.PhoneNumber = request.PhoneNumber;

        _clientRepository.Update(entity);

        return new UpdateClientResult {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber
        };
    }
}

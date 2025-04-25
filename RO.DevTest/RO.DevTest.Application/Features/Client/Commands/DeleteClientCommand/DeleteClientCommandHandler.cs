using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Client.Commands.DeleteClientCommand;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Unit> {
    private readonly IClientRepository _clientRepository;

    public DeleteClientCommandHandler(IClientRepository clientRepository) {
        _clientRepository = clientRepository;
    }

    public Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken) {
        var entity = _clientRepository.Get(c => c.Id == request.Id);
        if (entity == null) {
            throw new KeyNotFoundException("Client not found");
        }

        _clientRepository.Delete(entity);

        return Task.FromResult(Unit.Value);
    }
}

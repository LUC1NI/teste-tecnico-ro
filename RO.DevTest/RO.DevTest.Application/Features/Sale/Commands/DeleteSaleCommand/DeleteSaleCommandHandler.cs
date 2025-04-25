using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sale.Commands.DeleteSaleCommand;

public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, Unit> {
    private readonly ISaleRepository _saleRepository;

    public DeleteSaleCommandHandler(ISaleRepository saleRepository) {
        _saleRepository = saleRepository;
    }

    public Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken) {
        var entity = _saleRepository.Get(s => s.Id == request.Id);
        if (entity == null) {
            throw new KeyNotFoundException("Sale not found");
        }

        _saleRepository.Delete(entity);

        return Task.FromResult(Unit.Value);
    }
}

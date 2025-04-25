using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit> {
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    public Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
        var entity = _productRepository.Get(p => p.Id == request.Id);
        if (entity == null) {
            throw new KeyNotFoundException("Product not found");
        }

        _productRepository.Delete(entity);

        return Task.FromResult(Unit.Value);
    }
}

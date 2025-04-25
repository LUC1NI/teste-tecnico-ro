using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult> {
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    public Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
        var entity = _productRepository.Get(p => p.Id == request.Id);
        if (entity == null) {
            throw new KeyNotFoundException("Product not found");
        }

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.StockQuantity = request.Stock;

        _productRepository.Update(entity);

        var result = new UpdateProductResult {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Stock = entity.StockQuantity
        };

        return Task.FromResult(result);
    }
}

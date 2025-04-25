using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult> {
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
        var entity = new RO.DevTest.Domain.Entities.Product {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.Stock
        };

        await _productRepository.CreateAsync(entity, cancellationToken);

        return new CreateProductResult {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Stock = entity.StockQuantity
        };
    }
}

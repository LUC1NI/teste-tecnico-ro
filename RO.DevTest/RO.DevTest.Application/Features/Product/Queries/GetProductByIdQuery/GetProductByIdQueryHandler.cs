using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto> {
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    public Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) {
        var entity = _productRepository.Get(p => p.Id == request.Id);
        if (entity == null) {
            return Task.FromResult<ProductDto>(null);
        }

        var dto = new ProductDto {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            Stock = entity.StockQuantity
        };

        return Task.FromResult(dto);
    }
}

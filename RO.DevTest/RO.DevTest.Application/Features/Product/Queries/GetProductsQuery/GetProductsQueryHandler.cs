using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedProductResult> {
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    public Task<PagedProductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken) {
        var query = _productRepository.GetAll();

        if (!string.IsNullOrEmpty(request.Filter)) {
            query = query.Where(p => p.Name.Contains(request.Filter) || p.Description.Contains(request.Filter));
        }

        if (!string.IsNullOrEmpty(request.SortBy)) {
            if (request.SortBy.ToLower() == "name") {
                query = request.SortDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
            } else if (request.SortBy.ToLower() == "price") {
                query = request.SortDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
            }
        }

        var totalCount = query.Count();

        var products = query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new ProductDto {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.StockQuantity
            })
            .ToList();

        var result = new PagedProductResult {
            TotalCount = totalCount,
            Products = products
        };

        return Task.FromResult(result);
    }
}

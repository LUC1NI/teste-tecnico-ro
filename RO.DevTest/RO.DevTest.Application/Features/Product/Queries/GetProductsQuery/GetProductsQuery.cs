using MediatR;
using System.Collections.Generic;

namespace RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;

public class GetProductsQuery : IRequest<PagedProductResult> {
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Filter { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
}

public class PagedProductResult {
    public int TotalCount { get; set; }
    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
}

public class ProductDto {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

using MediatR;
using System;

namespace RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;

public class GetProductByIdQuery : IRequest<ProductDto> {
    public Guid Id { get; set; }
}

public class ProductDto {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

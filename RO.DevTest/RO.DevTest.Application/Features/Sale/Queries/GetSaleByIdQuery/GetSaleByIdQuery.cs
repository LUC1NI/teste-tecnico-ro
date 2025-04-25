using MediatR;
using System;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSaleByIdQuery;

public class GetSaleByIdQuery : IRequest<SaleDto> {
    public Guid Id { get; set; }
}

public class SaleDto {
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
}

using MediatR;
using System;
using System.Collections.Generic;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSalesQuery;

public class GetSalesQuery : IRequest<PagedSaleResult> {
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class PagedSaleResult {
    public int TotalCount { get; set; }
    public IEnumerable<SaleDto> Sales { get; set; } = new List<SaleDto>();
}

public class SaleDto {
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
}

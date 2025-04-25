using MediatR;
using System;

namespace RO.DevTest.Application.Features.Sale.Queries.AnalyzeSalesQuery;

public class AnalyzeSalesQuery : IRequest<AnalyzeSalesResult> {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class AnalyzeSalesResult {
    public int TotalSalesCount { get; set; }
    public decimal TotalRevenue { get; set; }
    public ProductRevenue[] ProductRevenues { get; set; } = Array.Empty<ProductRevenue>();
}

public class ProductRevenue {
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
}

using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RO.DevTest.Application.Features.Sale.Queries.AnalyzeSalesQuery;

public class AnalyzeSalesQueryHandler : IRequestHandler<AnalyzeSalesQuery, AnalyzeSalesResult> {
    private readonly ISaleRepository _saleRepository;

    public AnalyzeSalesQueryHandler(ISaleRepository saleRepository) {
        _saleRepository = saleRepository;
    }

    public Task<AnalyzeSalesResult> Handle(AnalyzeSalesQuery request, CancellationToken cancellationToken) {
        var sales = _saleRepository.GetAll()
            .Where(s => s.SaleDate >= request.StartDate && s.SaleDate <= request.EndDate)
            .ToList();

        var totalSalesCount = sales.Count;
        var totalRevenue = sales.Sum(s => s.TotalAmount);

        var productRevenues = sales
            .SelectMany(s => s.SaleItems)
            .GroupBy(si => new { si.ProductId, si.Product.Name })
            .Select(g => new ProductRevenue {
                ProductId = g.Key.ProductId,
                ProductName = g.Key.Name,
                Revenue = g.Sum(si => si.UnitPrice * si.Quantity)
            })
            .ToArray();

        var result = new AnalyzeSalesResult {
            TotalSalesCount = totalSalesCount,
            TotalRevenue = totalRevenue,
            ProductRevenues = productRevenues
        };

        return Task.FromResult(result);
    }
}

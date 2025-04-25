using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSalesQuery;

public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, PagedSaleResult> {
    private readonly ISaleRepository _saleRepository;

    public GetSalesQueryHandler(ISaleRepository saleRepository) {
        _saleRepository = saleRepository;
    }

    public Task<PagedSaleResult> Handle(GetSalesQuery request, CancellationToken cancellationToken) {
        var query = _saleRepository.GetAll();

        if (request.StartDate.HasValue) {
            query = query.Where(s => s.SaleDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue) {
            query = query.Where(s => s.SaleDate <= request.EndDate.Value);
        }

        var totalCount = query.Count();

        var sales = query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(s => new SaleDto {
                Id = s.Id,
                ClientId = s.ClientId,
                SaleDate = s.SaleDate,
                TotalAmount = s.TotalAmount
            })
            .ToList();

        var result = new PagedSaleResult {
            TotalCount = totalCount,
            Sales = sales
        };

        return Task.FromResult(result);
    }
}

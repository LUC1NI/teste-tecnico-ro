using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sale.Queries.GetSaleByIdQuery;

public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, SaleDto> {
    private readonly ISaleRepository _saleRepository;

    public GetSaleByIdQueryHandler(ISaleRepository saleRepository) {
        _saleRepository = saleRepository;
    }

    public Task<SaleDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken) {
        var entity = _saleRepository.Get(s => s.Id == request.Id);
        if (entity == null) {
            return Task.FromResult<SaleDto>(null);
        }

        var dto = new SaleDto {
            Id = entity.Id,
            ClientId = entity.ClientId,
            SaleDate = entity.SaleDate,
            TotalAmount = entity.TotalAmount
        };

        return Task.FromResult(dto);
    }
}

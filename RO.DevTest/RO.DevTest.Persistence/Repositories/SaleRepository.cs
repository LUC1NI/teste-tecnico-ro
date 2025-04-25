using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Persistence;

namespace RO.DevTest.Persistence.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(DefaultContext context) : base(context)
    {
    }

    // Additional sale-specific repository methods can be implemented here
}

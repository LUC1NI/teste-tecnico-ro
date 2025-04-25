using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Persistence;
using System.Linq;

namespace RO.DevTest.Persistence.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(DefaultContext context) : base(context)
    {
    }

    public IQueryable<Sale> GetAll() {
        return Context.Set<Sale>();
    }
}

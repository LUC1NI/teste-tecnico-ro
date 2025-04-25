using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Persistence;
using System.Linq;

namespace RO.DevTest.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(DefaultContext context) : base(context)
    {
    }

    public IQueryable<Product> GetAll() {
        return Context.Set<Product>();
    }
}

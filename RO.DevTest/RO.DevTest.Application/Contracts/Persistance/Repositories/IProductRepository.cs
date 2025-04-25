using RO.DevTest.Domain.Entities;
using System.Linq;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    IQueryable<Product> GetAll();
}

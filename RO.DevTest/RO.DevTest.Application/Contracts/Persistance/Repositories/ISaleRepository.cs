using RO.DevTest.Domain.Entities;
using System.Linq;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
    IQueryable<Sale> GetAll();
}

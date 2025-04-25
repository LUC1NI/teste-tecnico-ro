using RO.DevTest.Domain.Entities;
using System.Linq;

namespace RO.DevTest.Application.Contracts.Persistance.Repositories;

public interface IClientRepository : IBaseRepository<Client>
{
    IQueryable<Client> GetAll();
}

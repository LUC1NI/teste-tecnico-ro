using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Persistence;
using System.Linq;

namespace RO.DevTest.Persistence.Repositories;

public class ClientRepository : BaseRepository<Client>, IClientRepository
{
    public ClientRepository(DefaultContext context) : base(context)
    {
    }

    public IQueryable<Client> GetAll() {
        return Context.Set<Client>();
    }
}

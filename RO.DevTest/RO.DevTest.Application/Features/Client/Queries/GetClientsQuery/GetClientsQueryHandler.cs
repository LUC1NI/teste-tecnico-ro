using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RO.DevTest.Application.Features.Client.Queries.GetClientsQuery;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, PagedClientResult> {
    private readonly IClientRepository _clientRepository;

    public GetClientsQueryHandler(IClientRepository clientRepository) {
        _clientRepository = clientRepository;
    }

    public Task<PagedClientResult> Handle(GetClientsQuery request, CancellationToken cancellationToken) {
        var query = _clientRepository.GetAll();

        if (!string.IsNullOrEmpty(request.Filter)) {
            query = query.Where(c => c.Name.Contains(request.Filter) || c.Email.Contains(request.Filter));
        }

        if (!string.IsNullOrEmpty(request.SortBy)) {
            if (request.SortBy.ToLower() == "name") {
                query = request.SortDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name);
            } else if (request.SortBy.ToLower() == "email") {
                query = request.SortDescending ? query.OrderByDescending(c => c.Email) : query.OrderBy(c => c.Email);
            }
        }

        var totalCount = query.Count();

        var clients = query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new ClientDto {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            })
            .ToList();

        var result = new PagedClientResult {
            TotalCount = totalCount,
            Clients = clients
        };

        return Task.FromResult(result);
    }
}

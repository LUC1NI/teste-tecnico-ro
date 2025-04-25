using MediatR;
using System.Collections.Generic;

namespace RO.DevTest.Application.Features.Client.Queries.GetClientsQuery;

public class GetClientsQuery : IRequest<PagedClientResult> {
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Filter { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
}

public class PagedClientResult {
    public int TotalCount { get; set; }
    public IEnumerable<ClientDto> Clients { get; set; } = new List<ClientDto>();
}

public class ClientDto {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

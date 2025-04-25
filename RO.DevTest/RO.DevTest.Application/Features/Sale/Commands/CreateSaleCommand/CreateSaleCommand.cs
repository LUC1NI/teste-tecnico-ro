using MediatR;
using System;
using System.Collections.Generic;

namespace RO.DevTest.Application.Features.Sale.Commands.CreateSaleCommand;

public class CreateSaleCommand : IRequest<CreateSaleResult> {
    public Guid ClientId { get; set; }
    public DateTime SaleDate { get; set; }
    public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();
}

public class SaleItemDto {
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateSaleResult {
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public DateTime SaleDate { get; set; }
    public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();
}

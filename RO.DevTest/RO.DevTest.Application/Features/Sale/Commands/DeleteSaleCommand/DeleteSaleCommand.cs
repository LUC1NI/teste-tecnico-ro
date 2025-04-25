using MediatR;
using System;

namespace RO.DevTest.Application.Features.Sale.Commands.DeleteSaleCommand;

public class DeleteSaleCommand : IRequest<MediatR.Unit> {
    public Guid Id { get; set; }
}

using MediatR;
using System;

namespace RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;

public class DeleteProductCommand : IRequest<MediatR.Unit> {
    public Guid Id { get; set; }
}

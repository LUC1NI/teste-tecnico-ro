using MediatR;
using System;

namespace RO.DevTest.Application.Features.Client.Commands.DeleteClientCommand;

public class DeleteClientCommand : IRequest<MediatR.Unit> {
    public Guid Id { get; set; }
}

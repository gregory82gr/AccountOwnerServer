using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Commands.Owners
{
    public record CreateOwnerCommand(Owner Owner) : IRequest;
}

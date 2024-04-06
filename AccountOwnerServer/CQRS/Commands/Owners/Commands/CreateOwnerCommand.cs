using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Commands.Owners.Commands
{
    public record CreateOwnerCommand(Owner Owner) : IRequest;
}

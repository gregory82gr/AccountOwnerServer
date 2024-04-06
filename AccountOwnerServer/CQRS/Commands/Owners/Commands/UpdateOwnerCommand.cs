using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Commands.Owners.Commands
{

    public record UpdateOwnerCommand(Owner Owner) : IRequest;
}

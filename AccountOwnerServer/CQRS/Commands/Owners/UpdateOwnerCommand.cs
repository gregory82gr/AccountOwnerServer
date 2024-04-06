using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Commands.Owners
{
    public record UpdateOwnerCommand(Owner Owner) : IRequest;
}

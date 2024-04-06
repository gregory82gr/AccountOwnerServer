using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Commands.Owners
{
    public record DeleteOwnerCommand(Owner owner) : IRequest;
}

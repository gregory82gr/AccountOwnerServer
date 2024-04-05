using Entities.Models;
using MediatR;

namespace AccountOwnerServer.Owners.Commands
{
    public record DeleteOwnerCommand(Owner owner) : IRequest;
}

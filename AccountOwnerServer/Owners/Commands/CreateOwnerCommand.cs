using Entities.Models;
using MediatR;

namespace AccountOwnerServer.Owners.Commands
{
    public record CreateOwnerCommand(Owner Owner) : IRequest;
}

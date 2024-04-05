using Entities.Models;
using MediatR;

namespace AccountOwnerServer.Owners.Commands
{
    
    public record UpdateOwnerCommand(Owner Owner) : IRequest;
}

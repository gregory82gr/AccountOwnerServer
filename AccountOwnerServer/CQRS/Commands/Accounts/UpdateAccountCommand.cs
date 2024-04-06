using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Commands.Accounts
{
    public record UpdateAccountCommand(Account account) : IRequest;
}

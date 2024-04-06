using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Commands.Accounts
{
    public record CreateAccountCommand(Account account) : IRequest;
}

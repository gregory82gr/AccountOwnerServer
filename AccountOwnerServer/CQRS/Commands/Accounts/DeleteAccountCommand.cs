using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Commands.Accounts
{
    public record DeleteAccountCommand(Account account) : IRequest;
}

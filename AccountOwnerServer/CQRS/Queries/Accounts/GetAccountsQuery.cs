using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Queries.Accounts
{
    public record GetAccountsQuery() : IRequest<IEnumerable<Account>>;
}

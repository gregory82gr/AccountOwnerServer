using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Queries.Accounts
{
    public record GetAccountsByOwnerQuery : IRequest<IEnumerable<Account>>
    {
        public Guid Id { get; set; }
    }
}

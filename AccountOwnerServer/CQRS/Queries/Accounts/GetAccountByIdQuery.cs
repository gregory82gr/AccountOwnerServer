using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Queries.Accounts
{
    public record GetAccountByIdQuery : IRequest<Account>
    {
        public Guid Id { get; set; }
    }
}

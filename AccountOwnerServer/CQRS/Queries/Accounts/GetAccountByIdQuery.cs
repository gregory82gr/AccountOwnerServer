using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Queries.Accounts
{
    public class GetAccountByIdQuery : IRequest<Account>
    {
        public Guid Id { get; set; }
    }
}

using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Queries.Owners.Queries
{
    public class GetOwnerByIdQuery : IRequest<Owner>
    {
        public Guid Id { get; set; }
    }
}

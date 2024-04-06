using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Queries.Owners.Queries
{
    public class GetOwnerById : IRequest<Owner>
    {
        public Guid Id { get; set; }
    }
}

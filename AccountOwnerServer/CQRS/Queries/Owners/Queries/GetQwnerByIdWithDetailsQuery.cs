using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Queries.Owners.Queries
{
    public class GetQwnerByIdWithDetailsQuery : IRequest<Owner>
    {
        public Guid Id { get; set; }
    }
}

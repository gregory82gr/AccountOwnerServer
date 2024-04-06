using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Queries.Owners
{
    public record GetQwnerByIdWithDetailsQuery : IRequest<Owner>
    {
        public Guid Id { get; set; }
    }
}

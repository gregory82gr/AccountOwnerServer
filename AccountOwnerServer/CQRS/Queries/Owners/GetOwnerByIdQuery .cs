using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Queries.Owners
{
    public record GetOwnerByIdQuery : IRequest<Owner>
    {
        public Guid Id { get; set; }
    }
}

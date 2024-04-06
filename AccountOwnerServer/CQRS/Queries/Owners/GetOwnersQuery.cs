using Entities.Models;
using MediatR;
using System.Collections;

namespace AccountOwnerServer.CQRS.Queries.Owners
{
    public record GetOwnersQuery() : IRequest<IEnumerable<Owner>>;
}

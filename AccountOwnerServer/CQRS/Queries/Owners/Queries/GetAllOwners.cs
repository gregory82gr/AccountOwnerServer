using Entities.Models;
using MediatR;
using System.Collections;

namespace AccountOwnerServer.CQRS.Queries.Owners.Queries
{
    public record GetOwnersQuery() : IRequest<IEnumerable<Owner>>;
}

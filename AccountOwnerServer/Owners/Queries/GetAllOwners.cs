using Entities.Models;
using MediatR;
using System.Collections;

namespace AccountOwnerServer.Owners.Queries
{
    public record GetOwnersQuery() : IRequest<IEnumerable<Owner>>;
}

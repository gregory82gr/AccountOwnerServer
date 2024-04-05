using Entities.Models;
using MediatR;

namespace AccountOwnerServer.Owners.Queries
{
    public class GetOwnerById :IRequest<Owner>
    {
        public Guid Id { get; set; }
    }
}

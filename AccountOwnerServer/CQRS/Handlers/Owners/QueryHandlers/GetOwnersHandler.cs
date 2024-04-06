using AccountOwnerServer.CQRS.Queries.Owners;
using Contracts;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.Handlers.Owners.QueryHandlers
{
    public class GetOwnersHandler : IRequestHandler<GetOwnersQuery, IEnumerable<Owner>>
    { 
        private IUnitOfWork _unitOfWork;
        public GetOwnersHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<IEnumerable<Owner>> Handle(GetOwnersQuery request,
            CancellationToken cancellationToken) => await _unitOfWork.RepositoryWrapper.Owner.GetAllOwnersAsync();
    }
}

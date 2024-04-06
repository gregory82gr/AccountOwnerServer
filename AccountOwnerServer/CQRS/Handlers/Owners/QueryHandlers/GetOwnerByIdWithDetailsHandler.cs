using AccountOwnerServer.CQRS.Queries.Owners;
using Contracts;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Owners.QueryHandlers
{
    public class GetOwnerByIdWithDetailsHandler : IRequestHandler<GetQwnerByIdWithDetailsQuery, Owner>
    {
        private IUnitOfWork _unitOfWork;
        public GetOwnerByIdWithDetailsHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<Owner> Handle(GetQwnerByIdWithDetailsQuery request, CancellationToken cancellationToken) =>
            await _unitOfWork.RepositoryWrapper.Owner.GetOwnerWithDetailsAsync(request.Id);
    }
}

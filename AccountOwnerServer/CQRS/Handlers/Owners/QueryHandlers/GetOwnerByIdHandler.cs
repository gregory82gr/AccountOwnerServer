using AccountOwnerServer.CQRS.Queries.Owners;
using Contracts;
using Entities.Models;
using MediatR;
using System;

namespace AccountOwnerServer.Handlers.Owners.QueryHandlers
{
    public class GetOwnerByIdHandler : IRequestHandler<GetOwnerByIdQuery, Owner>
    {
        private IUnitOfWork _unitOfWork;
        public GetOwnerByIdHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<Owner> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken) =>
            await _unitOfWork.RepositoryWrapper.Owner.GetOwnerByIdAsync(request.Id);      
    }
}

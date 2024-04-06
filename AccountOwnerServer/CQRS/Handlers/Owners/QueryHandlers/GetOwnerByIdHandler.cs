using AccountOwnerServer.CQRS.Queries.Owners.Queries;
using Contracts;
using Entities.Models;
using MediatR;
using System;

namespace AccountOwnerServer.Handlers.Owners.QueryHandlers
{
    public class GetOwnerByIdHandler : IRequestHandler<GetOwnerById, Owner>
    {
        private readonly IRepositoryWrapperAsync _ownerRepository;

        public GetOwnerByIdHandler(IRepositoryWrapperAsync ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Owner> Handle(GetOwnerById request, CancellationToken cancellationToken)
        {
            return await _ownerRepository.Owner.GetOwnerByIdAsync(request.Id);
        }
    }
}

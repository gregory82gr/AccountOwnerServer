using AccountOwnerServer.CQRS.Queries.Owners.Queries;
using Contracts;
using Entities.Models;
using MediatR;
using System;

namespace AccountOwnerServer.Handlers.Owners.QueryHandlers
{
    public class GetOwnerByIdHandler : IRequestHandler<GetOwnerByIdQuery, Owner>
    {
        private readonly IRepositoryWrapperAsync _ownerRepository;

        public GetOwnerByIdHandler(IRepositoryWrapperAsync ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Owner> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _ownerRepository.Owner.GetOwnerByIdAsync(request.Id);
        }
    }
}

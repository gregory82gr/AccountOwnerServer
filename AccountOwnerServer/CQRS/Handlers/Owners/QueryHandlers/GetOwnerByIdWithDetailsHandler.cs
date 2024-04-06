using AccountOwnerServer.CQRS.Queries.Owners.Queries;
using Contracts;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Owners.QueryHandlers
{
    public class GetOwnerByIdWithDetailsHandler : IRequestHandler<GetQwnerByIdWithDetailsQuery, Owner>
    {
        private readonly IRepositoryWrapperAsync _ownerRepository;

        public GetOwnerByIdWithDetailsHandler(IRepositoryWrapperAsync ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Owner> Handle(GetQwnerByIdWithDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _ownerRepository.Owner.GetOwnerWithDetailsAsync(request.Id);
        }
    }
}

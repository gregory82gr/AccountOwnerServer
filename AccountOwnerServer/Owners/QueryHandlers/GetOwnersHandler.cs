using AccountOwnerServer.Owners.Queries;
using Contracts;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.Owners.QueryHandlers
{
    public class GetOwnersHandler : IRequestHandler<GetOwnersQuery, IEnumerable<Owner>>
    {
        private readonly IRepositoryWrapperAsync _ownerRepository;
        public GetOwnersHandler(IRepositoryWrapperAsync ownerRepository) => _ownerRepository = ownerRepository;
        public async Task<IEnumerable<Owner>> Handle(GetOwnersQuery request,
            CancellationToken cancellationToken) => await _ownerRepository.Owner.GetAllOwnersAsync();
    }
}

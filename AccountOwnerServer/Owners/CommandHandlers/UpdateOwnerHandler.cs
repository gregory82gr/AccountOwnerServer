using AccountOwnerServer.Owners.Commands;
using Contracts;
using MediatR;

namespace AccountOwnerServer.Owners.CommandHandlers
{
    public class UpdateOwnerHandler : IRequestHandler<UpdateOwnerCommand>
    {
        private readonly IRepositoryWrapperAsync _ownerRepository;
        public UpdateOwnerHandler(IRepositoryWrapperAsync ownerRepository) => _ownerRepository = ownerRepository;

        public async Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            _ownerRepository.Owner.UpdateOwner(request.Owner);
            await _ownerRepository.SaveAsync(cancellationToken);
            return;
        }
    }
}

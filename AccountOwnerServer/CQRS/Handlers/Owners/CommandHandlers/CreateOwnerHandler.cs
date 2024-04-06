using AccountOwnerServer.CQRS.Commands.Owners.Commands;
using Contracts;
using MediatR;

namespace AccountOwnerServer.Handlers.Owners.CommandHandlers
{
    public class CreateOwnerHandler : IRequestHandler<CreateOwnerCommand>
    {
        private readonly IRepositoryWrapperAsync _ownerRepository;
        public CreateOwnerHandler(IRepositoryWrapperAsync ownerRepository) => _ownerRepository = ownerRepository;

        public async Task Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            _ownerRepository.Owner.CreateOwner(request.Owner);
            await _ownerRepository.SaveAsync(cancellationToken);
            return;
        }
    }
}

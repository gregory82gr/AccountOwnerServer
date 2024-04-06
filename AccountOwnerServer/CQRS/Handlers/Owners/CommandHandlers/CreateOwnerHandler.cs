using AccountOwnerServer.CQRS.Commands.Owners;
using Contracts;
using MediatR;

namespace AccountOwnerServer.Handlers.Owners.CommandHandlers
{
    public class CreateOwnerHandler : IRequestHandler<CreateOwnerCommand>
    {
        private IUnitOfWork _unitOfWork;
        public CreateOwnerHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.RepositoryWrapper.Owner.CreateOwner(request.Owner);
            await _unitOfWork.RepositoryWrapper.SaveAsync(cancellationToken);
            return;
        }
    }
}

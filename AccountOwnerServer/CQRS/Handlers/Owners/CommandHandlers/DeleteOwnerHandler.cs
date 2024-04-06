using AccountOwnerServer.CQRS.Commands.Owners;
using Contracts;
using MediatR;

namespace AccountOwnerServer.Handlers.Owners.CommandHandlers
{

    public class DeleteOwnerHandler : IRequestHandler<DeleteOwnerCommand>
    {
        private IUnitOfWork _unitOfWork;
        public DeleteOwnerHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.RepositoryWrapper.Owner.DeleteOwner(request.owner);
            await _unitOfWork.RepositoryWrapper.SaveAsync(cancellationToken);
            return;
        }
    }
}

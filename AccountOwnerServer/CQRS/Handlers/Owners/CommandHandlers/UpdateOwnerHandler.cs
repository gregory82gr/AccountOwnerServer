using AccountOwnerServer.CQRS.Commands.Owners;
using Contracts;
using MediatR;

namespace AccountOwnerServer.Handlers.Owners.CommandHandlers
{
    public class UpdateOwnerHandler : IRequestHandler<UpdateOwnerCommand>
    {
        private IUnitOfWork _unitOfWork;
        public UpdateOwnerHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;  
        public async Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.RepositoryWrapper.Owner.UpdateOwner(request.Owner);
            await _unitOfWork.RepositoryWrapper.SaveAsync(cancellationToken);
            return;
        }
    }
}

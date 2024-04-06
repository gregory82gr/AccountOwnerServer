using AccountOwnerServer.CQRS.Commands.Accounts;
using AccountOwnerServer.CQRS.Commands.Owners;
using Contracts;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.CommandHandlers
{
    
    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand>
    {
        private IUnitOfWork _unitOfWork;
        public UpdateAccountHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        
        public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.RepositoryWrapper.Account.UpdateAccount(request.account);
            await _unitOfWork.RepositoryWrapper.SaveAsync(cancellationToken);
            return;
        }
    }
}
    
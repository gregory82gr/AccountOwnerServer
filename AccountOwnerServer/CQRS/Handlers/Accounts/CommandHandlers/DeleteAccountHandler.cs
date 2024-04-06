using AccountOwnerServer.CQRS.Commands.Accounts;
using AccountOwnerServer.CQRS.Commands.Owners;
using Contracts;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.CommandHandlers
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand>
    {
        private IUnitOfWork _unitOfWork;
        public DeleteAccountHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.RepositoryWrapper.Account.DeleteAccount(request.account);
            await _unitOfWork.RepositoryWrapper.SaveAsync(cancellationToken);
            return;
        }
    }
}

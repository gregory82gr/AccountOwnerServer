using AccountOwnerServer.CQRS.Commands.Accounts;
using AccountOwnerServer.CQRS.Commands.Owners;
using Contracts;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.CommandHandlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand>
    {
        private IUnitOfWork _unitOfWork;
        public CreateAccountHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.RepositoryWrapper.Account.CreateAccount(request.account);
            await _unitOfWork.RepositoryWrapper.SaveAsync(cancellationToken);
            return;
        }
    }
}

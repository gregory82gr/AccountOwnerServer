using AccountOwnerServer.CQRS.Commands.Accounts;
using AccountOwnerServer.CQRS.Commands.Owners;
using Contracts;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.CommandHandlers
{
    
    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand>
    {
        private readonly IRepositoryWrapperAsync _accountRepository;
        public UpdateAccountHandler(IRepositoryWrapperAsync accountRepository) => _accountRepository = accountRepository;
        public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            _accountRepository.Account.UpdateAccount(request.account);
            await _accountRepository.SaveAsync(cancellationToken);
            return;
        }
    }
}

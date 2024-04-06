using AccountOwnerServer.CQRS.Commands.Accounts;
using AccountOwnerServer.CQRS.Commands.Owners;
using Contracts;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.CommandHandlers
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly IRepositoryWrapperAsync _accountRepository;
        public DeleteAccountHandler(IRepositoryWrapperAsync accountRepository) => _accountRepository = accountRepository;
        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            _accountRepository.Account.DeleteAccount(request.account);
            await _accountRepository.SaveAsync(cancellationToken);
            return;
        }
    }
}

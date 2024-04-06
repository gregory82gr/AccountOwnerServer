using AccountOwnerServer.CQRS.Commands.Accounts;
using AccountOwnerServer.CQRS.Commands.Owners;
using Contracts;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.CommandHandlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand>
    {
        private readonly IRepositoryWrapperAsync _accountRepository;
        public CreateAccountHandler(IRepositoryWrapperAsync accountRepository) => _accountRepository = accountRepository;

        public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            _accountRepository.Account.CreateAccount(request.account);
            await _accountRepository.SaveAsync(cancellationToken);
            return;
        }
    }
}

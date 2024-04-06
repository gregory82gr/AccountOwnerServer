using AccountOwnerServer.CQRS.Queries.Accounts;
using Contracts;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.QueryHandlers
{
    public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, IEnumerable<Account>>
    {
        private readonly IRepositoryWrapperAsync _accountRepository;
        public GetAccountsHandler(IRepositoryWrapperAsync accountRepository) => _accountRepository = accountRepository;
        public async Task<IEnumerable<Account>> Handle(GetAccountsQuery request,
            CancellationToken cancellationToken) => await _accountRepository.Account.GetAllAccountsAsync();
    }
}

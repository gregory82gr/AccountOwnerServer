using AccountOwnerServer.CQRS.Queries.Accounts;
using AccountOwnerServer.CQRS.Queries.Owners.Queries;
using Contracts;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.QueryHandlers
{
    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, Account>
    {
        private readonly IRepositoryWrapperAsync _accountRepository;

        public GetAccountByIdHandler(IRepositoryWrapperAsync accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            return await _accountRepository.Account.GetAccountByIdAsync(request.Id);
        }
    }
}

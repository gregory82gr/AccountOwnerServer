using AccountOwnerServer.CQRS.Queries.Accounts;
using Contracts;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.QueryHandlers
{
    public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, IEnumerable<Account>>
    {
        private IUnitOfWork _unitOfWork;
        public GetAccountsHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<IEnumerable<Account>> Handle(GetAccountsQuery request,
            CancellationToken cancellationToken) => await _unitOfWork.RepositoryWrapper.Account.GetAllAccountsAsync();
    }
}

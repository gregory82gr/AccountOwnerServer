using AccountOwnerServer.CQRS.Queries.Accounts;
using AccountOwnerServer.CQRS.Queries.Owners.Queries;
using Contracts;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.QueryHandlers
{
    public class GetAccountsByOwnerHandler : IRequestHandler<GetAccountsByOwnerQuery, IEnumerable<Account>>
    {
        private IUnitOfWork _unitOfWork;
        public GetAccountsByOwnerHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Account>> Handle(GetAccountsByOwnerQuery request, CancellationToken cancellationToken) =>
           await _unitOfWork.RepositoryWrapper.Account.AccountsByOwnerAsync(request.Id);
    }
}

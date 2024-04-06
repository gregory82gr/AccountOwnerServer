using AccountOwnerServer.CQRS.Queries.Accounts;
using Contracts;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Handlers.Accounts.QueryHandlers
{
    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, Account>
    {
        private IUnitOfWork _unitOfWork;
        public GetAccountByIdHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)=>
                await _unitOfWork.RepositoryWrapper.Account.GetAccountByIdAsync(request.Id);
        
    }
}

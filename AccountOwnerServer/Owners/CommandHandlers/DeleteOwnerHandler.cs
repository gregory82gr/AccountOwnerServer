﻿using AccountOwnerServer.Owners.Commands;
using Contracts;
using MediatR;

namespace AccountOwnerServer.Owners.CommandHandlers
{
   
    public class DeleteOwnerHandler : IRequestHandler<DeleteOwnerCommand>
    {
        private readonly IRepositoryWrapperAsync _ownerRepository;
        public DeleteOwnerHandler(IRepositoryWrapperAsync ownerRepository) => _ownerRepository = ownerRepository;
        public async Task Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            _ownerRepository.Owner.DeleteOwner(request.owner);
            await _ownerRepository.SaveAsync(cancellationToken);
            return;
        }
    }
}
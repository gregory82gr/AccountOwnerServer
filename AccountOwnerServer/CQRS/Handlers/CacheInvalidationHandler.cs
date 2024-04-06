using AccountOwnerServer.CQRS.Notifications;
using Contracts;
using MediatR;

namespace AccountOwnerServer.Handlers
{

    public class CacheInvalidationHandler : INotificationHandler<OwnerAddedNotification>
    {
        private readonly IRepositoryWrapperAsync _ownerRepository;
        public CacheInvalidationHandler(IRepositoryWrapperAsync ownerRepository) => _ownerRepository = ownerRepository;
        public async Task Handle(OwnerAddedNotification notification, CancellationToken cancellationToken)
        {
            await _ownerRepository.Owner.EventOccured(notification.owner, "Cache Invalidated");
            await Task.CompletedTask;
        }
    }
}

using AccountOwnerServer.Notifications;
using Contracts;
using MediatR;

namespace AccountOwnerServer.Handlers
{
    public class EmailHandler : INotificationHandler<OwnerAddedNotification>
    {
        private readonly IRepositoryWrapperAsync _ownerRepository;
        public EmailHandler(IRepositoryWrapperAsync ownerRepository) => _ownerRepository = ownerRepository;
        public async Task Handle(OwnerAddedNotification notification, CancellationToken cancellationToken)
        {
            await _ownerRepository.Owner.EventOccured(notification.owner, "Email sent");
            await Task.CompletedTask;
        }
    }
}

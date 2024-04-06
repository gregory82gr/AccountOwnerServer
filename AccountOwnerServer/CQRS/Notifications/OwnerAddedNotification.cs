using Entities.DataTransferObjects;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.CQRS.Notifications
{

    public record OwnerAddedNotification(OwnerDto owner) : INotification;
}

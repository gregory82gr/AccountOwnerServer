using Entities.DataTransferObjects;
using Entities.Models;
using MediatR;

namespace AccountOwnerServer.Notifications
{
   
    public record OwnerAddedNotification(OwnerDto owner) : INotification;
}

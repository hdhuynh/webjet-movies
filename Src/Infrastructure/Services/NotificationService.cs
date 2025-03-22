using Webjet.Application.Common.Interfaces;
using Webjet.Application.Customers.EventHandlers;

namespace Webjet.Infrastructure.Services;

public class NotificationService : INotificationService
{
    public Task SendAsync(MessageDto message)
    {
        return Task.CompletedTask;
    }
}

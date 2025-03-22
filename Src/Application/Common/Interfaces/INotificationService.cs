using Webjet.Application.Customers.EventHandlers;
using System.Threading.Tasks;

namespace Webjet.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendAsync(MessageDto message);
}

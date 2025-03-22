using Webjet.Backend.Customers.EventHandlers;
using System.Threading.Tasks;

namespace Webjet.Backend.Common.Interfaces;

public interface INotificationService
{
    Task SendAsync(MessageDto message);
}

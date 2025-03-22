using System.Threading.Tasks;

namespace Webjet.Backend.Common.Interfaces;

public interface IUserManager
{
    Task<string?> CreateUserAsync(string userName, string password);
}

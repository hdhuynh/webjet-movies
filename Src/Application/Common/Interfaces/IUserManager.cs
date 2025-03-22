using System.Threading.Tasks;

namespace Webjet.Application.Common.Interfaces;

public interface IUserManager
{
    Task<string?> CreateUserAsync(string userName, string password);
}

using Store.Core.DTO;
using System.Threading.Tasks;

namespace Store.Core.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDTO userForAuth);
        Task<string> CreateToken();
    }
}

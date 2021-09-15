using Store.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Core.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
        Task<IList<string>> GetRoles();
    }
}

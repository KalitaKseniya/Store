using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Core.DTO;
using Store.Core.Entities;
using Store.Core.Interfaces;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        public AuthenticationController(ILoggerManager logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromQuery]UserForCreationDTO userForCreationDTO)
        {
            if(userForCreationDTO == null)
            {
                _logger.Error("UserForCreation can't be null.");
                return BadRequest("UserForCreation can't be null.");
            }
            User user = new User
            {
                FirstName = userForCreationDTO.FirstName,
                LastName = userForCreationDTO.LastName,
                Email = userForCreationDTO.Email,
                UserName = userForCreationDTO.UserName,
                PhoneNumber = userForCreationDTO.PhoneNumber,
                NormalizedUserName = userForCreationDTO.NormalizedUserName
            };

            var result = await _userManager.CreateAsync(user, userForCreationDTO.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            if (userForCreationDTO.Roles != null)
            {
                await _userManager.AddToRolesAsync(user, userForCreationDTO.Roles);
            }
            return Created("Created", user);
        }
    }
}

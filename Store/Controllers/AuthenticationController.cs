using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Store.Core.DTO;
using Store.Core.Entities;
using Store.Core.Interfaces;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ILoggerManager logger, UserManager<User> userManager,
            IAuthenticationManager authManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _authManager = authManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Authentication of the user
        /// </summary>
        /// <returns>Token if the user exists</returns>
        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserForAuthenticationDto userForAuth)
        {
            if (userForAuth != null && await _authManager.ValidateUser(userForAuth))
            {
                var response = new
                {
                    token = await _authManager.CreateToken(),
                    minutesExpires = _configuration.GetSection("jwtSettings").GetSection("minutesExpires").Value
                };
                return Ok(response);
            }
            _logger.Warn($"{nameof(AuthenticateUser)} Authenication failed. Username or password is incorrect");

            return Unauthorized();
        }

        /// <summary>
        /// Registration of the user
        /// </summary>
        [HttpPost("register"), Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto userForCreationDTO)
        {
            if (userForCreationDTO == null)
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

            foreach (var role in userForCreationDTO.Roles)
            {
                //now Administrator and Manager roles are available
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    _logger.Warn($"Specified role(s)={role} not found in db.");
                    return BadRequest("Specified role(s) not found in db.");
                }
            }

            var result = await _userManager.CreateAsync(user, userForCreationDTO.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRolesAsync(user, userForCreationDTO.Roles);

            return Created("Created", user);
        }
    }
}

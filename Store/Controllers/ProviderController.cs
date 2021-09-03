using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Core.DTO;
using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Core.RequestFeatures;

namespace Store.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/providers")]
    public class ProviderController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IProviderRepository _providerRepository;
        public ProviderController(ILoggerManager logger,
                                  IProviderRepository providerRepository)
        {
            _logger = logger;
            _providerRepository = providerRepository;
        }

        /// <summary>
        /// Get the list of all providers 
        /// </summary>
        [HttpGet]
        public IActionResult GetProviders([FromQuery]ProviderParams providerParams)
        {
            var providers = _providerRepository.Get(providerParams);
            if (providers == null)
            {
                _logger.Info("There are no providers in db");
                return NotFound();
            }
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(providers.MetaData));
            return Ok(providers);
        }

        /// <summary>
        /// Get the provider by id
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetProvider(int id)
        {
            var provider = _providerRepository.GetById(id);
            if (provider == null)
            {
                _logger.Info($"There is no provider with id={id} in db.");
                return NotFound($"There is no provider with id={id} in db.");
            }

            return Ok(provider);
        }

        /// <summary>
        /// Create a provider
        /// </summary>
        [HttpPost, Authorize(Roles = UserRoles.Administrator)]
        public IActionResult CreateProvider(ProviderForManipulationDto providerDto)
        {
            if (providerDto == null)
            {
                _logger.Warn("ProviderDro can't be null");
                return BadRequest("ProviderDro can't be null");
            }

            var provider = new Provider()
            {
                ImgPath = providerDto.ImgPath,
                Latitude = providerDto.Latitude,
                Longitude = providerDto.Longitude,
                Name = providerDto.Name,
                Info = providerDto.Info
            };

            _providerRepository.Create(provider);
            _providerRepository.Save();
            return StatusCode(201, new { provider });
        }

        /// <summary>
        /// Update the provider with the specified id 
        /// </summary>
        [HttpPut("{id}"), Authorize(Roles = UserRoles.Administrator)]

        public IActionResult UpdateProvider(int id, ProviderForManipulationDto providerDto)
        {
            if (providerDto == null)
            {
                _logger.Warn("ProviderDro can't be null");
                return BadRequest("ProviderDro can't be null");
            }
            var provider = _providerRepository.GetById(id);
            if (provider == null)
            {
                _logger.Warn($"There is no provider with id = {id}.");
                return NotFound($"There is no provider with id = {id}.");
            }

            provider.ImgPath = providerDto.ImgPath;
            provider.Latitude = providerDto.Latitude;
            provider.Longitude = providerDto.Longitude;
            provider.Name = providerDto.Name;
            provider.Info = providerDto.Info;

            _providerRepository.Update(provider);
            _providerRepository.Save();
            return NoContent();
        }

        /// <summary>
        /// Delete the provider with the specified id 
        /// </summary>
        [HttpDelete("{id}"), Authorize(Roles = UserRoles.Administrator)]
        public IActionResult DeleteProvider(int id)
        {
            var provider = _providerRepository.GetById(id);
            if (provider == null)
            {
                _logger.Warn($"There is no provider with id = {id}.");
                return NotFound($"There is no provider with id = {id}.");
            }

            _providerRepository.Delete(provider);
            _providerRepository.Save();
            return NoContent();
        }
    }
}
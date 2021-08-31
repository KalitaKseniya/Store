using Microsoft.AspNetCore.Mvc;
using Store.Core.Interfaces;

namespace Store.Controllers
{
    [ApiController]
    [ApiVersion("v1")]
    [Route("api/v{version}/[controller]")]
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
    
        [HttpGet]
        public IActionResult GetProviders()
        {
            var providers = _providerRepository.Get();
            if (providers == null)
            {
                _logger.Info("There are no providers in db");
                return NotFound();
            }
            return Ok(providers);
        }

        [HttpGet("{id}")]
        public IActionResult GetProvider(int id)
        {
            var provider = _providerRepository.GetById(id);
            if(provider == null)
            {
                _logger.Info($"There is no provider with id={id} in db.");
                return NotFound();
            }

            return Ok(provider);
        }

        [HttpPost("{id}")]
        public IActionResult CreateProvider(ProviderForCreationDto providerDto)
        {

        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Interfaces;
using System.Threading.Tasks;

namespace Store.V2.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/categories/{category_id}/products")]
    public class ProductV2Controller : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrencyService _currencyService;

        public ProductV2Controller(ILoggerManager logger, IProductRepository productRepository,
            ICategoryRepository categoryRepository, ICurrencyService currencyService)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _currencyService = currencyService;
        }

        /// <summary>
        /// Get the product by id for the specified category.
        /// You could choose city (write in Russian) and currency.
        ///  Allowed curr: USD, EUR, RUB, GBP, CAD, PLN, UAH, SEK, CHF, JPY, CNY, CZK, NOK.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrency(int category_id, int id, [FromQuery] string curr = "BYN", [FromQuery] string city = "Полоцк")
        {
            var category = _categoryRepository.GetById(category_id);
            if (category == null)
            {
                _logger.Error($"There is no category with id = {category_id} in db.");
                return NotFound($"There is no category with id = {category_id} in db.");
            }

            var product = _productRepository.GetForCategoryById(category_id, id);
            if (product == null)
            {
                _logger.Error($"There is no product with id = {id} for the category with id = {category_id} in db.");
                return NotFound($"There is no product with id = {id} for the category with id = {category_id} in db.");
            }

            var productForCurrencyDTO = await _currencyService.GetProductForCurrency(product, curr, city);
            if (productForCurrencyDTO == null)
            {
                _logger.Error($"There is no information about curr = {curr} in {city} or curr course = 0.");
                return NotFound($"There is no information about curr = {curr} in {city} or curr course = 0.");
            }
            return Ok(productForCurrencyDTO);
        }
    }
}

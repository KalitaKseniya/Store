using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Core.DTO;
using Store.Core.Entities;
using Store.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users/{user_id}/shopping_carts")]

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILoggerManager _logger;
        public ShoppingCartController(IShoppingCartRepository cartRepository,
                                      UserManager<User> userManager,
                                      ILoggerManager logger,
                                      IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(string user_id, int productId, int quantity = 1)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if(user == null)
            {
                return NotFound();
            }
            var product = _productRepository.GetById(productId);
            if(product == null)
            {
                return NotFound();
            }

            if(quantity < 1)
            {
                return BadRequest();
            }

            ShoppingCartItem item = new ShoppingCartItem()
            {
                Quantity = quantity,
                UserId = user_id,
                ProductId = productId,
                User = user,
                Product = product
            };
            _cartRepository.AddItem(item);
            _cartRepository.Save();
            var itemToReturn = new ShoppingCartItemDto()
            {
                Id = item.Id,
                ProductId = item.ProductId,
                UserId = item.UserId,
                Quantity = item.Quantity
            };
            return Ok(itemToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetItems(string user_id)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user == null)
            {
                return NotFound();
            }

            var items = _cartRepository.GetItems(user_id);
            var itemsToReturn = items.Select(x => new
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UserId = x.UserId,
                Quantity = x.Quantity
            });
            return Ok(itemsToReturn);

        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart(string user_id)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user == null)
            {
                return NotFound();
            }
            _cartRepository.ClearShoppingCart(user_id);
            _cartRepository.Save();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string user_id, int id)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user == null)
            {
                return NotFound();
            }
            var item = _cartRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            _cartRepository.DeleteItem(item);
            _cartRepository.Save();
            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuantity(string user_id, int id, int quantity)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user == null)
            {
                return NotFound();
            }

            var item = _cartRepository.GetByIdForUser(id, user_id);
            if(item == null)
            {
                return NotFound();
            }

            _cartRepository.UpdateQuantity(id, quantity);
            _cartRepository.Save();
            return Ok();
        }
    }
}

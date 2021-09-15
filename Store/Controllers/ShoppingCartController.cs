using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Core.DTO;
using Store.Core.Entities;
using Store.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Authorize(Roles = UserRoles.Client)]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/shopping_carts")]

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

        [HttpGet("hello")]
        public async Task<IActionResult> GetItem(int productId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var item = _cartRepository.GetByProductIdForUser(productId, user.Id);
            var itemToReturn = new ShoppingCartItemDto()
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                ProductName = item.Product.Name,
                ProductPrice = item.Product.Price
            };
                
            return Ok(itemToReturn);
        }

        /// <summary>
        /// For the authorized user 
        /// add a product to shopping cart (if wasn't in user's shopping cart)   
        /// or update its quantity (if exists in user's shopping cart)  
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddItem(int productId, int quantity = 1)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(user == null)
            {
                return Unauthorized();
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
                UserId = user.Id,
                ProductId = productId,
                User = user,
                Product = product
            };

            var itemInDb = _cartRepository.GetByProductIdForUser(productId, user.Id);
            if (itemInDb == null)
            {
                  _cartRepository.AddItem(item);
            }
            else
            {
                _cartRepository.UpdateQuantity(item, quantity + item.Quantity);
            }
           
            _cartRepository.Save();
            var itemToReturn = new ShoppingCartItemDto()
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                ProductName = product.Name,
                ProductPrice = product.Price
            };
            return Ok(itemToReturn);
        }

        /// <summary>
        /// Get all shopping cart items of the authorized user 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }

            var items = _cartRepository.GetItems(user.Id);
            var itemsToReturn = items.Select(item => new
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                ProductName = item.Product.Name,
                ProductPrice = item.Product.Price
            });
            return Ok(itemsToReturn);

        }


        /// <summary>
        /// Delete all shopping cart items for the authorized user 
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            _cartRepository.ClearShoppingCart(user.Id);
            _cartRepository.Save();
            return Ok();
        }

        /// <summary>
        /// Delete shopping cart item by id for the authorized user 
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            var item = _cartRepository.GetByIdForUser(id, user.Id);
            if (item == null)
            {
                return NotFound();
            }

            _cartRepository.DeleteItem(item);
            _cartRepository.Save();
            return Ok();
        }

        /// <summary>
        /// Update the quantity of ShoppingCartItem with id = id for the authorized user 
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }

            var item = _cartRepository.GetByIdForUser(id, user.Id);
            if(item == null)
            {
                return NotFound();
            }
            if(quantity < 0)
            {
                return BadRequest();
            }
            _cartRepository.UpdateQuantity(item, quantity);
            _cartRepository.Save();
            return Ok();

        }
    }
}

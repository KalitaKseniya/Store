using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Store.Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly RepositoryContext _repository;
        public ShoppingCartRepository(RepositoryContext repository)
        {
            _repository = repository;
        }

        public ShoppingCartItem GetByIdForUser(int id, string userId)
        {
            return _repository.Items.FirstOrDefault(i => i.Id == id && i.UserId == userId);
        }
        
        public ShoppingCartItem GetByProductIdForUser(int productId, string userId)
        {
            var toReturn = _repository.Items.Include(i => i.Product)
                                      .FirstOrDefault(i => i.ProductId == productId && i.UserId == userId);
            return toReturn;
        }

        public void AddItem(ShoppingCartItem item)
        {
            _repository.Items.Add(item);
        }

        public void ClearShoppingCart(string userId)
        {
            var userItems = GetItems(userId);
            if(userItems != null)
            {
                _repository.RemoveRange(userItems);
            }
        }

        public void DeleteItem(ShoppingCartItem item)
        {
            _repository.Remove(item);
        }

        public List<ShoppingCartItem> GetItems(string userId)
        {
            return _repository.Items.Include(i => i.Product)
                                    .Where(i => i.UserId == userId)
                                    .ToList();
        }

        public void UpdateQuantity(ShoppingCartItem item, int quantity)
        {
            if(quantity == 0)
            {
                DeleteItem(item);
            }
            else
            {
                item.Quantity = quantity;
            }
        }

        public void Save()
        {
            _repository.SaveChanges();
        }
    }
}

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

        public ShoppingCartItem GetById(int id)
        {
            return _repository.Items.FirstOrDefault(i => i.Id == id);
        }
        
        public ShoppingCartItem GetByIdForUser(int id, string userId)
        {
            return _repository.Items.FirstOrDefault(i => i.Id == id && i.UserId == userId);
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
            return _repository.Items.Where(i => i.UserId == userId).ToList();
        }

        public void UpdateQuantity(int id, int quantity)
        {
            var item = GetById(id);
            item.Quantity = quantity;
            _repository.Items.Update(item);
        }
        public void Save()
        {
            _repository.SaveChanges();
        }
    }
}

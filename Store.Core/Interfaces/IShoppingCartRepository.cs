using Store.Core.Entities;
using System.Collections.Generic;

namespace Store.Core.Interfaces
{
    public interface IShoppingCartRepository
    {
        void AddItem(ShoppingCartItem item);
        void UpdateQuantity(int id, int quantity);
        ShoppingCartItem GetById(int id);
        ShoppingCartItem GetByIdForUser(int id, string userId);
        List<ShoppingCartItem> GetItems(string userId);
        void ClearShoppingCart(string userId);
        void Save();
        void DeleteItem(ShoppingCartItem item);
    }
}

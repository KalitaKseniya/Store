using Store.Core.Entities;
using System.Collections.Generic;

namespace Store.Core.Interfaces
{
    public interface IShoppingCartRepository
    {
        void AddItem(ShoppingCartItem item);
        void UpdateQuantity(ShoppingCartItem item, int quantity);
        ShoppingCartItem GetByIdForUser(int id, string userId);
        ShoppingCartItem GetByProductIdForUser(int productId, string userId);
        List<ShoppingCartItem> GetItems(string userId);
        void ClearShoppingCart(string userId);
        void Save();
        void DeleteItem(ShoppingCartItem item);
    }
}

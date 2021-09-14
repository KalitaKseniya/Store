using Store.Core.Entities;

namespace Store.Core.Interfaces
{
    public interface IShoppingCartRepository
    {
        void AddItem(Product product, int quantity);
        void UpdateQuantity(Product product, int quantity);
        void RemoveItem(Product product);
        void Clear();
        void ComputeTotalValue();
    }
}

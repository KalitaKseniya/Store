using Store.Core.Entities;
using System.Threading.Tasks;
using Store.Core.DTO;

namespace Store.Core.Interfaces
{
    public interface ICurrencyService
    {
        Task<ProductForCurrencyDTO> GetProductForCurrency(Product product, string curr, string city);
    }
}

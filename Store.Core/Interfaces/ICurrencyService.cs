using Store.Core.DTO;
using Store.Core.Entities;
using System.Threading.Tasks;

namespace Store.Core.Interfaces
{
    public interface ICurrencyService
    {
        Task<ProductForCurrencyDto> GetProductForCurrency(Product product, string curr, string city);
    }
}

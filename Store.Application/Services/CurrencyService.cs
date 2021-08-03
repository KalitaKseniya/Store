using Newtonsoft.Json;
using Store.Core.DTO;
using Store.Core.Entities;
using Store.Core.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Store.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        //ToDo вынести в конфиг
        private string urlTemplate = "https://belarusbank.by/api/kursExchange?city={city}";

        private Dictionary<string, decimal> _currDict { get; set; }
        public CurrencyService()
        {
            //get url from config
        }
        public async Task<ProductForCurrencyDTO> GetProductForCurrency(Product product, string curr, string city)
        {
            string url = urlTemplate.Replace("{city}", city);
            decimal convertedPrice = 1;
            if (curr.ToUpper() != "BYN")
            {
                await UpdateCurrDict(url);

                if (_currDict.TryGetValue(curr.ToUpper(), out convertedPrice) == false ||
                                                               convertedPrice == 0)
                {
                    return null;
                }
            }

            return GetConvertedProduct(product, curr, convertedPrice);
        }
        #region protected methods
        protected async Task UpdateCurrDict(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = await reader.ReadToEndAsync();
                    var curr = JsonConvert.DeserializeObject<List<Currency>>(json).FirstOrDefault();
                    _currDict = curr.GetType()
                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                    .ToDictionary(prop => (string)prop.Name, prop => (decimal)prop.GetValue(curr, null));
                }
            }
            response.Close();
        }
        protected ProductForCurrencyDTO GetConvertedProduct(Product product, string currName, decimal currPrice)
        {
            return new ProductForCurrencyDTO
            {
                Curr = currName,
                Description = product.Description,
                Name = product.Name,
                Price = Convert(currPrice, product.Price)
            };
        }
        protected decimal Convert(decimal currPrice, decimal initPrice)
        {
            return initPrice / currPrice;
        }
        #endregion
    }
}

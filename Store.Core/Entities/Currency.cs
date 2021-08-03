using Newtonsoft.Json;

namespace Store.Core.Entities
{
    public class Currency
    {
        #region properies
        [JsonProperty("USD_out")]
        public decimal USD { get; set; } // курс продажи структурным подразделением Доллар США
        [JsonProperty("EUR_out")]
        public decimal EUR { get; set; } // курс продажи структурным подразделением Евро
        [JsonProperty("RUB_out")]
        public decimal RUB { get; set; } // курс продажи структурным подразделением(100 ед.) Российский рубль
        [JsonProperty("GBP_out")]
        public decimal GBP { get; set; } // курс продажи структурным подразделением Фунт стерлингов
        [JsonProperty("CAD_out")]
        public decimal CAD { get; set; } // курс продажи структурным подразделением Канадский доллар
        [JsonProperty("PLN_out")]
        public decimal PLN { get; set; } // курс продажи структурным подразделением Польский злотый
        [JsonProperty("UAH_out")]
        public decimal UAH { get; set; } // курс продажи структурным подразделением(100 ед.) Украинская гривна
        [JsonProperty("SEK_out")]
        public decimal SEK { get; set; } // курс продажи структурным подразделением(10 ед.) Шведская крона
        [JsonProperty("CHF_out")]
        public decimal CHF { get; set; } // курс продажи структурным подразделением(10 ед.) Швейцарский франк
        [JsonProperty("JPY_out")]
        public decimal JPY { get; set; } // курс продажи структурным подразделением(100 ед.) Японская иена
        [JsonProperty("CNY_out")]
        public decimal CNY { get; set; } // курс продажи структурным подразделением(10 ед.) Китайский юань
        [JsonProperty("CZK_out")]
        public decimal CZK { get; set; } // курс продажи структурным подразделением(100 ед.) Чешская крона
        [JsonProperty("NOK_out")]
        public decimal NOK { get; set; } // курс продажи структурным подразделением(10 ед.) Норвежская крона
        #endregion

        [JsonConstructor]
        public Currency(decimal USD_out, decimal EUR_out, decimal RUB_out, decimal GBP_out, decimal CAD_out,
                        decimal PLN_out, decimal UAH_out, decimal SEK_out, decimal CHF_out, decimal JPY_out,
                        decimal CNY_out, decimal CZK_out, decimal NOK_out
            )
        {
            USD = USD_out;
            EUR = EUR_out;
            RUB = RUB_out / 100;
            GBP = GBP_out;
            CAD = CAD_out;
            PLN = PLN_out;
            UAH = UAH_out / 100;
            SEK = SEK_out / 10;
            CHF = CHF_out / 10;
            JPY = JPY_out / 100;
            CNY = CNY_out / 10;
            CZK = CZK_out / 100;
            NOK = NOK_out / 10;
        }
    }
}

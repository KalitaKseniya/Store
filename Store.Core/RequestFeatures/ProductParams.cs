namespace Store.Core.RequestFeatures
{
    public class ProductParams : RequestParams
    {
        public string Search { get; set; }
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = int.MaxValue;
        public bool IsPriceRangeValid()
        {
            return MinPrice <= MaxPrice;
        }
    }
}

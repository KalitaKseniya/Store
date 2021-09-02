
namespace Store.Core.RequestFeatures
{
    public abstract class RequestParams
    {
        private int pageSize = 10;
        private const int maxPageSize = 50;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; } = "Id";
        public string OrderDir { get; set; } = "ASC";
    }
}

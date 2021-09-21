
namespace Store.Core.Shared
{
    public abstract class Entity<T>
    {
        public T EntityId { get; set; }
        public string Operation { get; set; }
        public string Type { get; set; }
    }
}

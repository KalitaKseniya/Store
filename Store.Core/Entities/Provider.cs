
namespace Store.Core.Entities
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImgPath { get; set; }
    }
}

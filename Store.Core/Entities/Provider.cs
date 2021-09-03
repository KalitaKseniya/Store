
using System.ComponentModel.DataAnnotations;

namespace Store.Core.Entities
{
    public class Provider
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImgPath { get; set; }
        public string Info { get; set; }
    }
}

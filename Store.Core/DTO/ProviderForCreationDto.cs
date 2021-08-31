
using System.ComponentModel.DataAnnotations;

namespace Store.Core.DTO
{
    public class ProviderForCreationDto
    {
        [Required(ErrorMessage = "")]
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImgPath { get; set; }
    }
}

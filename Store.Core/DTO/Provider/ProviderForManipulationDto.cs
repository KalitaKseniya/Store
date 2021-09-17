using System.ComponentModel.DataAnnotations;

namespace Store.Core.DTO
{
    public class ProviderForManipulationDto
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string ImagePath { get; set; }
        public string Info { get; set; }
    }
}
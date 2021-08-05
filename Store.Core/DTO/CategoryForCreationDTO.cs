using System.ComponentModel.DataAnnotations;

namespace Store.Core.DTO
{
    public class CategoryForCreationDto
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }
    }
}

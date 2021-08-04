using System.ComponentModel.DataAnnotations;

namespace Store.Core.DTO
{
    public class CategoryForCreationDTO
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }
    }
}

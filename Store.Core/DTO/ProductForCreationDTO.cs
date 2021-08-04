﻿using System.ComponentModel.DataAnnotations;

namespace Store.Core.DTO
{
    public class ProductForCreationDTO
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

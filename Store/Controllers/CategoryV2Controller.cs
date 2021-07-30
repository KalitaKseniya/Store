using Microsoft.AspNetCore.Mvc;
using Store.Core.Interfaces;
using Store.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    //curr->currency
    public class CategoryV2Controller : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILoggerManager _logger;
        public CategoryV2Controller(ICategoryRepository categoryRepository, ILoggerManager logger)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok("hello");
        }
    }
}

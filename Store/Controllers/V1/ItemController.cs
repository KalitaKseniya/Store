using EventBus.Base.Standard;
using Microsoft.AspNetCore.Mvc;
using Store.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers.V1
{
    [ApiController]
    [Route("api/v1/items")]
    public class ItemController : Controller
    {
        private readonly IEventBus _eventBus;

        public ItemController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost]
        public IActionResult Publish()
        {
            var message = new ItemCreatedIntegrationEvent("Item title", "Item description");

            _eventBus.Publish(message);

            return Ok();
        }
    }
}

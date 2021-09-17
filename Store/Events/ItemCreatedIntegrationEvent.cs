using EventBus.Base.Standard;

namespace Store.Events
{
    public class ItemCreatedIntegrationEvent : IntegrationEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ItemCreatedIntegrationEvent(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
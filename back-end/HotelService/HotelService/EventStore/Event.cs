using Newtonsoft.Json;

namespace HotelService.EventStore
{
    public class Event
    {
        public int Id { get; set; }
        public string Aggregate { get; set; }
        public int Version { get; set; }
        public string EventType { get; set; }
        public string EventData { get; set; }

        public static Event Create(string aggregate, int Version, IEvent @event) => new()
        {
            Aggregate = aggregate,
            Version = Version,
            EventType = @event.GetType().Name,
            EventData = JsonConvert.SerializeObject(@event)
        };
    }
}

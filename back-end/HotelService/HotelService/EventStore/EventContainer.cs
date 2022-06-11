using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace HotelService.EventStore
{
    public class EventContainer
    {
        public int Id { get; set; }
        public string Aggregate { get; set; }
        public int Version { get; set; }
        public string EventType { get; set; }
        public string EventData { get; set; }

        public static EventContainer Create(string aggregate, int Version, IEvent @event) => new()
        {
            Aggregate = aggregate,
            Version = Version,
            EventType = @event.GetType().FullName,
            EventData = JsonConvert.SerializeObject(@event)
        };

        public IEvent Deserialize()
        {
            var eventType = Type.GetType(EventType, true);

            return (IEvent) JObject.Parse(EventData).ToObject(eventType);
        }
    }
}

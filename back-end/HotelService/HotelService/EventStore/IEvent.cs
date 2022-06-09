using System;

namespace HotelService.EventStore
{
    public interface IEvent
    {
        string Id { get; set; }
        DateTime Timestamp { get; set; }
    }
}

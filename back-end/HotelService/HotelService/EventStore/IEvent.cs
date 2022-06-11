using System;

namespace HotelService.EventStore
{
    public interface IEvent
    {
        DateTime Timestamp { get; set; }
    }
}

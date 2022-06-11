using System;

namespace HotelService.EventStore
{
    public class RoomAvailable : IEvent
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int RoomNumber { get; set; }
        public bool Available = true;
    }
}

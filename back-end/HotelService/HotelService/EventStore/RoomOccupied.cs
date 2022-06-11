using System;

namespace HotelService.EventStore
{
    public class RoomOccupied : IEvent
    {
        public int RoomNumber { get; set; }
        public bool Available = false;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}

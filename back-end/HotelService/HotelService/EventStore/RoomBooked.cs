using System;

namespace HotelService.EventStore
{
    public class RoomBooked : IEvent
    {
        public string Id { get; set; }

        public int RoomNumber { get; set; }
        public bool Reserved = true;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}

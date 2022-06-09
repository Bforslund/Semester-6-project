using System;

namespace HotelService.EventStore
{
    public class RoomFree : IEvent
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int RoomNumber { get; set; }
        public bool Reserved = false;
    }
}

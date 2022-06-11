using System;

namespace HotelService.EventStore
{
    public class RoomRegistered : IEvent
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool Available = true;
    }
}

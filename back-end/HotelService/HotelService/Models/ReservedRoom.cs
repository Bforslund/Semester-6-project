using System;

namespace HotelService.Models
{
    public class ReservedRoom
    {
        public int Id { get; set; }
        Random rnd = new Random();
        public Room Room { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public ReservedRoom(Room room, DateTime startDate, DateTime endDate)
        {
            Id = rnd.Next(1, 999);
            Room = room;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}

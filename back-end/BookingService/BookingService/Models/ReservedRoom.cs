using BookingService.Models;
using System;

namespace HotelService.Models
{
    public class ReservedRoom
    {
        public int Id { get; set; }
        
        public Room Room { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public ReservedRoom()
        {

        }
        public ReservedRoom(Room room, DateTime startDate, DateTime endDate)
        {
           
            Room = room;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}

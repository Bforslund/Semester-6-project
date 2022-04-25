using System;

namespace HotelService.Models
{
    public class ReservedRoom
    {
        public int Id { get; set; }
      
        public int RoomId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public ReservedRoom()
        {

        }
        public ReservedRoom(int room, DateTime startDate, DateTime endDate)
        {
          
            RoomId = room;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}

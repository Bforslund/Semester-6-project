using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class ReservedRoom
    {
        public ReservedRoom(int roomId, DateTime start, DateTime end)
        {
            RoomId = roomId;
            Start = start;
            End = end;
        }

        public int RoomId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelService.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int HotelId { get; set; }

        public int RoomId { get; set; }
        public string ContactInfo { get; set; }
        public DateTime End { get; set; }
        public DateTime Start { get; set; }

        public bool Confirmed { get; set; }
        public Booking(int id, int hotel, string info, DateTime end, DateTime start, int roomId)
        {
            Id = id;
            HotelId = hotel;
            RoomId = roomId;
            ContactInfo = info;
            End = end;
            Start = start;
            Confirmed = false;
        }
        public Booking() { }
    }
}

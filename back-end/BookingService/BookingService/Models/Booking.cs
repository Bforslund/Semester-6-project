using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingService.Models;

namespace Booking_service.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int HotelId { get; set; }

        public int RoomId { get; set; }
        public string ContactInfo { get; set; }
        public DateTime End { get; set; }
        public DateTime Start { get; set; }
        Random rnd = new Random();
        public bool Confirmed { get; set; }
        public Booking(int hotel, string info, DateTime end, DateTime start, int room)
        {
            Id = rnd.Next(1, 999);
            HotelId = hotel;
            RoomId = room;
            ContactInfo = info;
            End = end;
            Start = start;
            Confirmed = false;
        }
        public Booking() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelService.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        public string Info { get; set; }
        public Booking(int id, Hotel hotel, string info)
        {
            Id = id;
            Hotel = hotel;
            Info = info;
        }
    }
}

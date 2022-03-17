using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelService.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }

        public int Rooms { get; set; }
        public Hotel(int id, string title, string info, int rooms)
        {
            Id = id;
            Title = title;
            Info = info;
            Rooms = rooms;
        }
    }
}

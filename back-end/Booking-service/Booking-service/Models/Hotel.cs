using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_service.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public Hotel(int id, string title, string info)
        {
            Id = id;
            Title = title;
            Info = info;
        }
    }
}

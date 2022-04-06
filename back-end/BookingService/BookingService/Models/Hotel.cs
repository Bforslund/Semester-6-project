using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingService.Models;

namespace Booking_service.Models
{
    public class Hotel
    {
      public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();
        
        Random rnd = new Random();  
        public IDictionary<string, int> RoomsByType => CalculateRoomsByType();

        public Hotel(string title, string info)
        {
            Id = rnd.Next(1, 999);
            Title = title;
            Info = info;
        }
        public Hotel(int id, string title, string info)
        {
            Id = id;
            Title = title;
            Info = info;
        }
        private IDictionary<string, int> CalculateRoomsByType() => Rooms.DistinctBy(x => x.RoomType).ToDictionary(
                x => x.RoomType,
                x => Rooms.Count(y => y.RoomType == x.RoomType
        ));
    }
}

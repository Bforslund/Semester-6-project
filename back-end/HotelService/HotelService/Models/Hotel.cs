using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace HotelService.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();
        public IDictionary<string, int> RoomsByType => CalculateRoomsByType();

        public Hotel() { 
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

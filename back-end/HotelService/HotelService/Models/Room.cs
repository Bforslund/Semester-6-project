using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace HotelService.Models
{
    public class Room
    {
       
        private string roomType;

        public int Id { get; set; }

        public string RoomType { get => roomType; set => roomType = value; }
      Random rnd = new Random();    
        public Room(string roomType)
        {
            Id = rnd.Next(1, 999);
            RoomType = roomType;
           
        }
        public Room(int id, string roomType)
        {
            Id = id;
            RoomType = roomType;

        }

    }
}

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

        public Room(string roomType)
        {
            RoomType = roomType;
        }

        public Room()
        {

        }

    }
}

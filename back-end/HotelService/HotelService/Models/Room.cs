using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace HotelService.Models
{
    public class Room
    {
        private int id;
        private string roomType;

        public int Id { get => id; set => id = value; }
        public string RoomType { get => roomType; set => roomType = value; }
      
        public Room(int id, string roomType)
        {
            this.Id = id;
            this.RoomType = roomType;
           
        }

    }
}

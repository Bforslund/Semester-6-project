using System;

namespace HotelService.Models
{
    public class RoomProjection
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool Available { get; set; }

        public RoomProjection(int roomNumber, string roomType, bool available)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            Available = available;
        }

        public static RoomProjection Create(Room room)
        {
            return new RoomProjection(room.RoomNumber, room.RoomType, room.Available);
        }
    }
}

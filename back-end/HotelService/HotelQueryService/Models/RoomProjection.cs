using System;

namespace HotelQueryService.Models
{
    public class RoomProjection
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }
        public string RoomType { get; set; }

        public RoomProjection(int roomNumber, string roomType)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
        }
    }
}

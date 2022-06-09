namespace HotelService.Models
{
    public class RoomProjection
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool Reserved { get; set; }

        public RoomProjection(int roomNumber, string roomType, bool reserved)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            Reserved = reserved;
        }
    }
}

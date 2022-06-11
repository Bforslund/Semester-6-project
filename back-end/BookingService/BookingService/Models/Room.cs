namespace BookingService.Models
{
    public class Room
    {
        private string roomType;
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public string RoomType { get => roomType; set => roomType = value; }

        public Room(int roomNumber, string roomType)
        {
            RoomType = roomType;
            RoomNumber = roomNumber;
        }

        public Room()
        {

        }
    }
}

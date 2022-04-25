namespace HotelService.Models
{
    public class AddRoomEvent
    {
        public AddRoomEvent(int hotelId, Room room)
        {
            HotelId = hotelId;
            this.Room = room;
        }

        public int HotelId { get; set; }

        public Room Room { get; set; }


    }
}

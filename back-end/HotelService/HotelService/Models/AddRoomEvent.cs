namespace HotelService.Models
{
    public class AddRoomEvent
    {
        public AddRoomEvent(int hotelId, RoomProjection room)
        {
            HotelId = hotelId;
            this.Room = room;
        }

        public int HotelId { get; set; }

        public RoomProjection Room { get; set; }


    }
}

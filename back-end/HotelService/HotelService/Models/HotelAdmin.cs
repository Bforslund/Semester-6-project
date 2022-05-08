namespace HotelService.Models
{
    public class HotelAdmin
    {
        public HotelAdmin(int id, string username, string password, int hotelId)
        {
            Id = id;
            Username = username;
            Password = password;
            HotelId = hotelId;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int HotelId { get; set; }

     
    }
}

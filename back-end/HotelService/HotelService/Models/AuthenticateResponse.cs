namespace HotelService.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int HotelId { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(HotelAdmin hotelAdmin, string token)
        {
            Id = hotelAdmin.Id;
            Username = hotelAdmin.Username;
            HotelId = hotelAdmin.HotelId;
            Token = token;
        }
    }
}

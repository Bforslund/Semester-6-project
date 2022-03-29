using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking_service.Models;
using BookingService.Models;

namespace BookingService.Database
{
    public class MockDB
    {
        public static List<Hotel> hotelList = new List<Hotel>();
        public static List<Booking> BookingList = new List<Booking>();
        public static List<Room> RoomsList = new List<Room>();
        public static List<ReservedRoom> ReservedRoomsList = new List<ReservedRoom>();


        public void FillDB()
        {
            Room haagRoom = new Room(1, "Economy");
            Room amsRoom = new Room(3, "Economy");
            Room rdmRoom = new Room(5, "Economy");

            RoomsList.Add(haagRoom);
            RoomsList.Add(amsRoom);
            RoomsList.Add(rdmRoom);


            Hotel haagHotel = new Hotel(1, "Den Haag Hotel", "This hotel is beautiful located next to the beach");
            haagHotel.RoomsByType.Add(haagRoom, 8);

            Hotel amsHotel = new Hotel(2, "Amsterdam Hotel", "This hotel is located right next to clubs and is a great fit for the party person");
            amsHotel.RoomsByType.Add(amsRoom, 6);

            Hotel rdmHotel = new Hotel(3, "Rotterdam Hotel", "This hotel is located next to the highest buildings");
            rdmHotel.RoomsByType.Add(rdmRoom, 14);

            hotelList.Add(haagHotel);
            hotelList.Add(amsHotel);
            hotelList.Add(rdmHotel);

            DateTime dt1 = new DateTime(2015, 12, 05);
            DateTime dt2 = new DateTime(2015, 12, 10);
            DateTime dt3 = new DateTime(2015, 12, 09);
            DateTime dt4 = new DateTime(2015, 12, 17);
            DateTime dt5 = new DateTime(2015, 02, 02);
            DateTime dt6 = new DateTime(2015, 02, 04);
            DateTime start1 = new DateTime(2015, 12, 03);
            DateTime end2 = new DateTime(2015, 12, 12);

            //Booking b1 = new Booking(1, rdmHotel, "jfds", dt4, dt3);
            //Booking b2 = new Booking(2, rdmHotel, "jfds", dt2, dt1);
            //Booking b3 = new Booking(3, rdmHotel, "jfds", dt6, dt5);
            //BookingList.Add(b1);
            //BookingList.Add(b2);
            //BookingList.Add(b3);

        }


        public int AmountOfAvailableRooms(int hotelId, DateTime startNewBooking, DateTime endNewBooking)
        {
            var hotel = hotelList.Single(h => h.Id == hotelId);
            var bookings = GetAllBookingsOfOneHotel(hotel);

            var amountOfTakenRooms = bookings.Count(
                booking =>
                    !((endNewBooking < booking.Start && startNewBooking < booking.Start) ||
                    (booking.End < startNewBooking && booking.Start < startNewBooking)));

            return hotel.Rooms - amountOfTakenRooms;
        }

        public List<Booking> GetAllBookingsOfOneHotel(Hotel h)
        {
            List<Booking> bookingsof1hotel = new List<Booking>();
            foreach (Booking b in BookingList)
            {
                if (b.HotelId == h.Id)
                {
                    bookingsof1hotel.Add(b);
                }
            }
            return bookingsof1hotel;
        }

  




    }
}

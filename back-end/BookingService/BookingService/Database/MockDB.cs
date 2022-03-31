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
        public static List<Hotel> HotelList = new List<Hotel>();
        public static List<Booking> BookingList = new List<Booking>();
        public static List<Room> ReservedRoomsList = new List<Room>();


        public void FillDB()
        {


            Hotel haagHotel = new Hotel(1, "Den Haag Hotel", "This hotel is beautiful located next to the beach");
            haagHotel.RoomsByType.Add(new Room(1, "Economy"), 8);
            haagHotel.RoomsByType.Add(new Room(2, "Premium"), 2);

            Hotel amsHotel = new Hotel(2, "Amsterdam Hotel", "This hotel is located right next to clubs and is a great fit for the party person");
            amsHotel.RoomsByType.Add(new Room(3, "Economy"), 6);
            amsHotel.RoomsByType.Add(new Room(4, "Premium"), 1);

            Hotel rdmHotel = new Hotel(3, "Rotterdam Hotel", "This hotel is located next to the highest buildings");
            rdmHotel.RoomsByType.Add(new Room(5, "Economy"), 14);
            rdmHotel.RoomsByType.Add(new Room(6, "Premium"), 1);

            HotelList.Add(haagHotel);
            HotelList.Add(amsHotel);
            HotelList.Add(rdmHotel);

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
            var hotel = HotelList.Single(h => h.Id == hotelId);
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

        public Hotel GetHotelById(int id)
        {
            foreach (Hotel hotel in HotelList)
            {
                if (id == hotel.Id)
                {
                    return hotel;
                }
            }
            return null;
        }

        public Room GetRoomById(Hotel h, int id)
        {
            foreach (var room in h.RoomsByType.Keys)
            {
                if (id == room.Id)
                {
                    return room;
                }
            }
            return null;
        }

        public bool CheckAvailability(int hId, DateTime start, DateTime end)
        {
           if(AmountOfAvailableRooms(hId, start, end) > 1)
            {
                return true;
            }
            return false;
        }

        public bool ConfirmBooking(Booking b)
        {
           

            if (CheckAvailability(b.HotelId, b.Start, b.End))
            {
                var obj = BookingList.FirstOrDefault(x => x.Id == b.Id);
                if (obj != null)
                {
                    obj.Confirmed = true;
                }
            }
            return true;
           
        }






    }
}

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
            haagHotel.Rooms.Add(new Room(2, "Economy"));
            haagHotel.Rooms.Add(new Room("Economy"));
            haagHotel.Rooms.Add(new Room("Economy"));
            haagHotel.Rooms.Add(new Room("Economy"));

            haagHotel.Rooms.Add(new Room("Premium"));
            haagHotel.Rooms.Add(new Room("Premium"));
            haagHotel.Rooms.Add(new Room("Premium"));

            Hotel amsHotel = new Hotel("Amsterdam Hotel", "This hotel is located right next to clubs and is a great fit for the party person");
            amsHotel.Rooms.Add(new Room("Economy"));
            amsHotel.Rooms.Add(new Room("Premium"));

            Hotel rdmHotel = new Hotel("Rotterdam Hotel", "This hotel is located next to the highest buildings");
            rdmHotel.Rooms.Add(new Room("Economy"));
            rdmHotel.Rooms.Add(new Room("Premium"));

            HotelList.Add(haagHotel);
            HotelList.Add(amsHotel);
            HotelList.Add(rdmHotel);

            //DateTime dt1 = new DateTime(2015, 12, 05);
            //DateTime dt2 = new DateTime(2015, 12, 10);
            //DateTime dt3 = new DateTime(2015, 12, 09);
            //DateTime dt4 = new DateTime(2015, 12, 17);
            //DateTime dt5 = new DateTime(2015, 02, 02);
            //DateTime dt6 = new DateTime(2015, 02, 04);
            //DateTime start1 = new DateTime(2015, 12, 03);
            //DateTime end2 = new DateTime(2015, 12, 12);

            //Booking b1 = new Booking(1, 1, "jfds", System.DateTime.Now, System.DateTime.Now, 2);
            //Booking b2 = new Booking(2, 1, "jfds", dt2, dt1, 2);
            //Booking b3 = new Booking(3, 1, "jfds", dt6, dt5, 2);
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

            return hotel.RoomsByType.Sum(x => x.Value) - amountOfTakenRooms;
        }

        public List<Room> GetAvailableRooms(int hotelId, DateTime startNewBooking, DateTime endNewBooking)
        {
            var hotel = HotelList.Single(h => h.Id == hotelId);
            var bookings = GetAllBookingsOfOneHotel(hotel);
            List<int> roomsIDs = new List<int>();
            List<Room> availableRooms = new List<Room>();
            foreach (Room room in hotel.Rooms)
            {
                roomsIDs.Add(room.Id);
            }

            List<Booking> TakenRoomsAtCertainDates = bookings.FindAll(
                booking =>
                    !((endNewBooking < booking.Start && startNewBooking < booking.Start) ||
                    (booking.End < startNewBooking && booking.Start < startNewBooking)));

            foreach(Booking b in TakenRoomsAtCertainDates)
            {
                if (roomsIDs.Contains(b.RoomId))
                {
                    roomsIDs.Remove(b.RoomId);
                }
            }

            foreach(int id in roomsIDs)
            {
                Room r = GetRoomById(id,hotel);
                availableRooms.Add(r);
            }
            return availableRooms;
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

        public Room GetRoomById(int id, Hotel h)
        {
            foreach (Room room in h.Rooms)
            {
                if (id == room.Id)
                {
                    return room;
                }
            }
            return null;
        }

        public Room GetReservedRoomById(int id)
        {
            foreach (var room in ReservedRoomsList)
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

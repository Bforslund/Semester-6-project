using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelService.Models;

namespace HotelService.Database
{
    public class database
    {
        public static List<Hotel> HotelList = new List<Hotel>();
        public static List<ReservedRoom> ReservedRoomsList = new List<ReservedRoom>();
    

        public void fillDB()
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
       

            //ReservedRoom newReservedRoom = new ReservedRoom(new Room("Economy"), dt1, dt2);
            //ReservedRoom newReservedRoom2 = new ReservedRoom(new Room("Economy"), dt3, dt4);
            //ReservedRoomsList.Add(newReservedRoom);
            //ReservedRoomsList.Add(newReservedRoom2);
        }



        public void AddHotel(Hotel h)
        {
            HotelList.Add(h);
        }
        public void AddRoom(Hotel h, Room r)
        {
            h.Rooms.Add(r);
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
            foreach (var room in h.Rooms)
            {
                if (id == room.Id)
                {
                    return room;
                }
            }
            return null;
        }

        public bool CheckAvailability()
        {
            return true;
        }

        public bool ReserveRoom(Booking b)
        {
            if (CheckAvailability())
            {
                Hotel h = GetHotelById(b.HotelId);
                Room room = GetRoomById(h,b.RoomId);
                ReservedRoom newReservedRoom = new ReservedRoom(room, b.Start, b.End);
                ReservedRoomsList.Add(newReservedRoom);
                return true;
            }
            return false;
        }
        public IEnumerable<Room> GetRoomsOfHotel(Hotel h)
        {
            return h.Rooms;
        }
    }
}

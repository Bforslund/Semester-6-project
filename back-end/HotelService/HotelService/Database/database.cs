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
        public static List<Room> ReservedRoomsList = new List<Room>();
    

        public void fillDB()
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

        }



        public void AddHotel(Hotel h)
        {
            HotelList.Add(h);
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

        public bool CheckAvailability()
        {
            return true;
        }

        public bool ReserveRoom(Booking b)
        {
            Hotel h = GetHotelById((int)b.HotelId);
            if (CheckAvailability())
            {
                ReservedRoomsList.Add(b.RoomType);
               // h.RoomsByType[b.RoomType] -= 1;
                return true;
            }
            return false;
        }
    }
}

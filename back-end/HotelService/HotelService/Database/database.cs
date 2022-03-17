using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelService.Models;

namespace HotelService.Database
{
    public class database
    {
        public static List<Hotel> hotelList = new List<Hotel>();
        
        public void fillDB()
        {
            Hotel haagHotel = new Hotel(1, "Den Haag Hotel", "This hotel is beautiful located next to the beach", 10);
            Hotel amsHotel = new Hotel(2, "Den Amsterdam Hotel", "This hotel is located right next to clubs and is a great fit for the party person", 7);
            Hotel rdmHotel = new Hotel(3, "Den Rotterdam Hotel", "This hotel is located next to the highest buildings", 15);
            Hotel eindHotel = new Hotel(4, "Den Eindhoven Hotel", "This hotel is located next to the albert heijn", 23);

            hotelList.Add(haagHotel);
            hotelList.Add(amsHotel);
            hotelList.Add(rdmHotel);
            hotelList.Add(eindHotel);

        }

        public void removeRoom(Hotel h)
        {
            foreach (Hotel hotel in hotelList)
            {
                if (h.Id == hotel.Id)
                {
                    hotel.Rooms -= 1;
                }
            }
        }


        public void AddHotel(Hotel h)
        {
            hotelList.Add(h);
        }

        public Hotel GetHotelById(int id)
        {
            foreach (Hotel hotel in hotelList)
            {
                if (id == hotel.Id)
                {
                    return hotel;
                }
            }
            return null;
        }
    }
}

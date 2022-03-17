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
        
        public void AddHotel(Hotel h)
        {
            hotelList.Add(h);
        }
    }
}

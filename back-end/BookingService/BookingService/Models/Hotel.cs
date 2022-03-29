﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingService.Models;

namespace Booking_service.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public int Rooms => RoomsByType.Sum(x => x.Value);
        public IDictionary<Room, int> RoomsByType { get; } = new Dictionary<Room, int>();

        public Hotel(int id, string title, string info)
        {
            Id = id;
            Title = title;
            Info = info;
        }
    }
}

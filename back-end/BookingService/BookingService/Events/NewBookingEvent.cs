﻿using System;
using Booking_service.Models;

namespace Booking_service.Events
{
    internal class NewBookingEvent
    {
        private Booking booking;
        public int BookingNumber => booking.Id;
        public int Hotel => booking.HotelId;

        public string RoomType => booking.RoomType;
        public string ContactInfo => booking.ContactInfo;
        public DateTime End => booking.End;
        public DateTime Start => booking.Start;

        public NewBookingEvent(Booking booking)
        {
            this.booking = booking;
        }
    }
}
using Booking_service.Models;
using BookingService.Database;
using BookingService.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookingService.Tests;

public class UnitTest1
{

    //Check availability number of rooms free logic(BookingService)
    //Confirm a booking logic(BookingService)
    //Check availability room logic (HotelService)

    Database.BookingService database = new Database.BookingService();
    [Fact]
    public void TestAmountOfAvailableRooms()
    {
        //Check availability room logic(BookingService)
        database.FillDB();
        DateTime dt1 = new DateTime(2015, 12, 05);
        DateTime dt2 = new DateTime(2015, 12, 10);
        List<Room> amount = database.GetAvailableRooms(1, dt1, dt2);
        Assert.Equal(7, 7);
    }
  
}
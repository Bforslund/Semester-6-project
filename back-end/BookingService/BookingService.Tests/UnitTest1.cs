using Booking_service.Models;
using BookingService.Database;
using BookingService.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookingService.Tests;

public class UnitTest1
{
    MockDB database = new MockDB();
    [Fact]
    public void TestAmountOfAvailableRooms()
    {
        database.FillDB();
        DateTime dt1 = new DateTime(2015, 12, 05);
        DateTime dt2 = new DateTime(2015, 12, 10);
        List<Room> amount = database.GetAvailableRooms(1, dt1, dt2);
        Assert.Equal(7, 7);
    }
  
}
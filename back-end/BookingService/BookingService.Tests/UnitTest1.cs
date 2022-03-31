using Booking_service.Models;
using BookingService.Database;
using BookingService.Models;
using System;
using Xunit;

namespace BookingService.Tests;

public class UnitTest1
{
    MockDB database = new MockDB();
    [Fact]
    public void TestAmountOfAvailableRooms()
    {
        database.FillDB();
        Hotel h = database.GetHotelById(2);
        Room r = database.GetRoomById(h, 3);
      int amount=  database.AmountOfAvailableRooms(2, System.DateTime.Now, System.DateTime.Now);
        Assert.Equal(7, amount);
    }
    [Fact]
    public void TestAmountOfAvailableRoomsAfterBooking()
    {
       
        //Hotel h = database.GetHotelById(2);
        //Room r = database.GetRoomById(h, 3);
        //DateTime dt5 = new DateTime(2015, 02, 02);
        //DateTime dt6 = new DateTime(2015, 02, 04);
        //Booking b = new Booking(1, 2, "ajdjasd", dt5, dt6, r);

        //database.ReserveRoom(b);
        //int amount = database.AmountOfAvailableRooms(2, System.DateTime.Now, System.DateTime.Now);
        //Assert.Equal(7, amount);
    }
}
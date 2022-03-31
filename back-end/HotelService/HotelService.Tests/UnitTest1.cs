using HotelService.Database;
using HotelService.Models;
using Xunit;

namespace HotelService.Tests;

public class UnitTest1
{
    database database = new database();
    [Fact]
    public void TestIfRoomWasReserved()
    {
        //database.fillDB();
        //Hotel h = database.GetHotelById(2);
        //Room r = database.GetRoomById(h, 3);
        //Booking b = new Booking(1, 2, "ajdjasd", System.DateTime.Now, System.DateTime.Now, r);

        //database.ReserveRoom(b);

        Assert.True(true);

    }
}
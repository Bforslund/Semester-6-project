using Booking_service.Models;
using BookingService.Models;
using BookingService.Repository;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookingService.Tests;

public class UnitTest1
{
    private readonly AvalabilityService _availabilityService;
    private readonly Mock<ApplicationDbContext> _dbContextMock = new Mock<ApplicationDbContext>();
    private readonly Mock<ICipherService> _cipherService = new Mock<ICipherService>();

    public UnitTest1()
    {
        _availabilityService = new AvalabilityService(_dbContextMock.Object, _cipherService.Object);
    }

    [Fact]
    public async Task TestAmountOfAvailableRoomsAsync()
    {

        //Arange
        var hotel = new Hotel(1, "hotel1", "good hotel");
        var bookedRoom = new Room(100, "classic");
        hotel.Rooms.Add(bookedRoom);
        hotel.Rooms.Add(new Room(110, "premium"));

        _dbContextMock.Setup(x => x.Hotels).ReturnsDbSet(new List<Hotel> { hotel });

        _dbContextMock.Setup(x => x.Bookings)
            .ReturnsDbSet(new List<Booking> 
            { 
                new Booking(hotel.Id, "info", DateTime.Now + TimeSpan.FromDays(2), DateTime.Now - TimeSpan.FromDays(2), bookedRoom.RoomNumber) 
            });

        ////Act
        int amount = await _availabilityService.AmountOfAvailableRoomsAsync(1, DateTime.Now, DateTime.Now + TimeSpan.FromDays(1));
        //Assert

         Assert.Equal(1, amount);
    }

    [Fact]
    public async Task TestGettingAmountOfAvailableRoomsAsync()
    {

        //Arange
        var hotel = new Hotel(1, "hotel1", "good hotel");
        var bookedRoom = new Room(100, "classic");
        hotel.Rooms.Add(bookedRoom);
        hotel.Rooms.Add(new Room(110, "premium"));

        _dbContextMock.Setup(x => x.Hotels).ReturnsDbSet(new List<Hotel> { hotel });

        _dbContextMock.Setup(x => x.Bookings)
            .ReturnsDbSet(new List<Booking>
            {
                new Booking(hotel.Id, "info", DateTime.Now + TimeSpan.FromDays(2), DateTime.Now - TimeSpan.FromDays(2), bookedRoom.RoomNumber)
            });

        ////Act
       var rooms = await _availabilityService.GetAvailableRoomsAsync(1, DateTime.Now, DateTime.Now + TimeSpan.FromDays(1));
        //Assert

        Assert.Single(rooms);
    }

    [Fact]
    public async Task ComponentTest()
    {
        // Arrange
        MockHttpMessageHandler
            .When(HttpMethod.Get, "https://shopify/api/v1/products/admin/api/2021-07/products/APPLE_IPHONE.json")
            .WithHeaders(new Dictionary<string, string>
            {
                ["X-Shopify-Access-Token"] = "secret"
            })
            .Respond(MediaTypeNames.Application.Json, File.ReadAllText("Shopify/Examples/APPLE_IPHONE.json"));

        // Act #1
        HttpResponseMessage getAllResponse = await SystemUnderTest.GetAsync("/api/v1/orders");
        var orders = await getAllResponse.Content.ReadFromJsonAsync<Order[]>();
        orders.Length.Should().Be(0);

        // Act #2
        HttpResponseMessage createResponse = await SystemUnderTest.PostAsync(
            "/api/v1/orders",
            JsonContent.Create(new CreateOrderDto()
            {
                ProductNumbers = new[] { "APPLE_IPHONE" },
                UserId = Guid.NewGuid(),
                TotalAmount = 495m,
            }));

        // Act #3
        getAllResponse = await SystemUnderTest.GetAsync("/api/v1/orders");


        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getAllResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        orders = await getAllResponse.Content.ReadFromJsonAsync<Order[]>();
        orders.Length.Should().Be(1);
    }
}
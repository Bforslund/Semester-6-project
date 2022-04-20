using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking_service.Models;
using BookingService.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Messaging;

namespace BookingService.Repository
{
    public class AvalabilityService
    {
        
        private readonly ApplicationDbContext _context;

        public AvalabilityService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AmountOfAvailableRoomsAsync(int hotelId, DateTime startNewBooking, DateTime endNewBooking)
        {
            var hotel = await _context.Hotels.Where(a => a.Id == hotelId).Include(h => h.Rooms).FirstOrDefaultAsync();
            var bookings = await _context.Bookings.Where(b => b.HotelId == hotelId).ToListAsync();

            var amountOfTakenRooms = bookings.Count(
                booking => Between(startNewBooking, endNewBooking, booking));

            return hotel.RoomsByType.Sum(x => x.Value) - amountOfTakenRooms;
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(int hotelId, DateTime startNewBooking, DateTime endNewBooking)
        {
            var hotel = await _context.Hotels.Where(a => a.Id == hotelId).Include(h => h.Rooms).FirstOrDefaultAsync();
            var bookings = await _context.Bookings.Where(b => b.HotelId == hotelId).ToListAsync();

            var bookingsBetweenCertainDates = bookings.FindAll(
                booking => Between(startNewBooking, endNewBooking, booking));

            return hotel.Rooms.Where(room => !bookingsBetweenCertainDates.Any(booking => booking.RoomId == room.Id));
        }

        private static bool Between(DateTime startNewBooking, DateTime endNewBooking, Booking booking)
        {
            return !((endNewBooking < booking.Start && startNewBooking < booking.Start) ||
                                (booking.End < startNewBooking && booking.Start < startNewBooking));
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(booking => booking.Id == id);
        }

        public async Task<List<Booking>> GetBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task ConfirmBookingAsync(Booking booking)
        {
            var available = await AmountOfAvailableRoomsAsync(booking.HotelId, booking.Start, booking.End);
            if (available > 1)
            {
                booking.Confirmed = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            Booking newBooking = new Booking(booking.HotelId, booking.ContactInfo, booking.Start, booking.End, booking.RoomId);
            _context.Bookings.Add(newBooking);
            await _context.SaveChangesAsync();
            return newBooking;
        }
       
    }
       
       


    }


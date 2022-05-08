﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking_service.Models;
using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Repository
{
    public class AvalabilityService
    {
        
        private readonly ApplicationDbContext _context;
        private readonly CipherService _cipherService;

        public AvalabilityService(ApplicationDbContext context, CipherService cipherService)
        {
            _context = context;
            _cipherService = cipherService;
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
            Booking b = await _context.Bookings.FirstOrDefaultAsync(booking => booking.Id == id);
            b.ContactInfo = _cipherService.Decrypt(b.ContactInfo);
            return b;
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
            Booking newBooking = new Booking(booking.HotelId, _cipherService.Encrypt(booking.ContactInfo), booking.Start, booking.End, booking.RoomId);
            _context.Bookings.Add(newBooking);
            await _context.SaveChangesAsync();
            return newBooking;
        }
        public async Task<Booking> UpdateBookingAsync(Booking updatedBooking)
        {
            var existingBooking = _context.Bookings.First(a => a.Id == updatedBooking.Id);
            existingBooking.ContactInfo = _cipherService.Encrypt(updatedBooking.ContactInfo);
            await _context.SaveChangesAsync();
            return existingBooking;
        }

    }
       
       


    }

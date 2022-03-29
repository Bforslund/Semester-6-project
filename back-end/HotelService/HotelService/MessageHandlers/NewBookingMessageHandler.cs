﻿using HotelService.Controllers;
using HotelService.Database;
using HotelService.Models;
using Shared.Messaging;
using System.Threading.Tasks;

namespace PlayerService.MessageHandlers
{
    public class NewBookingMessageHandler : IMessageHandler<Booking>
    {

        database database = new database();
        private readonly IMessagePublisher _messagePublisher;

        public NewBookingMessageHandler( IMessagePublisher messagePublisher)
        {
           
            _messagePublisher = messagePublisher;
        }
        public Task HandleMessageAsync(string messageType, Booking obj)
        {
           
            if (obj == null)
            {
                return Task.CompletedTask;
            }

            //check if room is available
            // add to reserved rooms table

            //send a room reserved event
            _messagePublisher.PublishMessageAsync("BookingConfirmed", "confirmed");

            return Task.CompletedTask;
        }
    }
}

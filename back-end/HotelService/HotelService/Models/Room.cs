using HotelService.EventStore;
using System;
using System.Collections.Generic;

namespace HotelService.Models
{
    public class Room
    {   
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool Available { get; set; }

        internal int Version = 0;
        internal List<IEvent> PendingChanges { get; } = new();

        public Room(int roomNumber, string roomType)
        {
            ApplyEvent(new RoomRegistered
            {
                RoomNumber = roomNumber, 
                RoomType = roomType
            });
        }


        public Room(IEnumerable<IEvent> events)
        {
            // Replay events to get current state.
            foreach (var @event in events)
            {
                ApplyEvent(@event, isReplay: true);
            }
        }


        private void ApplyEvent(IEvent @event, bool isReplay = false)
        {
            if (isReplay)
            {
                Version++;
            }
            else
            {
                PendingChanges.Add(@event);
            }

            switch (@event)
            {
                case RoomRegistered roomRegistered:
                    HandleEvent(roomRegistered);
                    break;
                case RoomOccupied roomBooked:
                    HandleEvent(roomBooked);
                    break;

                case RoomAvailable roomFree:
                    HandleEvent(roomFree);
                    break;
            }
        }

        private void HandleEvent(RoomOccupied roomBooked)
        {
            RoomNumber = roomBooked.RoomNumber;
            Available = roomBooked.Available;
        }

        private void HandleEvent(RoomAvailable roomFree)
        {
            RoomNumber = roomFree.RoomNumber;
            Available = roomFree.Available;
        }
        private void HandleEvent(RoomRegistered roomRegistered)
        {
            RoomNumber = roomRegistered.RoomNumber;
            RoomType = roomRegistered.RoomType;
            Available = roomRegistered.Available;
        }

        public void RoomIsAvailable()
        {
            ApplyEvent(new RoomAvailable
            {
                RoomNumber = RoomNumber
            });
        }

        public void RoomIsOccupied()
        {
            if (!Available)
            {
                throw new ArgumentException($"Room {RoomNumber} is already occupied");
            }

            ApplyEvent(new RoomOccupied
            {
                RoomNumber = RoomNumber
            });
        }
    }
}

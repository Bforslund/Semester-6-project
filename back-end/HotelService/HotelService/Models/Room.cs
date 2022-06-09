using HotelService.EventStore;
using System;
using System.Collections.Generic;

namespace HotelService.Models
{
    public class Room
    {
        public string Id { get; set; }
      
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public bool Reserved { get; set; }

        internal int Version = 0;
        internal List<IEvent> PendingChanges { get; } = new();

        public Room(int roomNumber, string roomType)
        {
            Version++;
            ApplyEvent(new RoomRegistered
            {
                Id = $"room:{roomNumber}:{Version}:RoomRegistered",
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
                case RoomBooked roomBooked:
                    HandleEvent(roomBooked);
                    break;

                case RoomFree roomFree:
                    HandleEvent(roomFree);
                    break;
            }
        }

        private void HandleEvent(RoomBooked roomBooked)
        {
            RoomNumber = roomBooked.RoomNumber;
            Reserved = roomBooked.Reserved;
        }

        private void HandleEvent(RoomFree roomFree)
        {
            RoomNumber = roomFree.RoomNumber;
            Reserved = roomFree.Reserved;
        }
        private void HandleEvent(RoomRegistered roomRegistered)
        {
            RoomNumber = roomRegistered.RoomNumber;
            RoomType = roomRegistered.RoomType;
            Reserved = roomRegistered.Reserved;
        }

        public void EmptyRoom()
        {
            ApplyEvent(new RoomFree
            {
                Id = $"room:{RoomNumber}:{Version}:RoomFree",
                RoomNumber = RoomNumber
            });
        }
        public void ReserveRoom()
        {
            if (Reserved)
            {
                throw new ArgumentException($"Room {RoomNumber} is already reserved");
            }

            ApplyEvent(new RoomBooked
            {
                Id = $"room:{RoomNumber}:{Version}:RoomBooked",
                RoomNumber = RoomNumber
            });
        }
    }
}

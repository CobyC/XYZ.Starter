using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XYZ.Starter.Classes;
using XYZ.Starter.Data;

namespace XYZ.Starter.Data
{
    public static class AppDbContextExtensions
    {
        private static int seatId;
        private static int seatGridId;

        public static void SeedAppDbContext(this AppDbContext appDbContext)
        {
            seatId = 5;
            seatGridId = 1;
            //populate the database with some seed values for testing
            var meetUp1 = new MeetUp()
            {
                Id = 1,
                CostPerSeat = 0M,
                Location = "Location 1",
                Date = DateTime.Now.AddDays(2),
                SeatGrid = GenerateSeatGrid(10, 10)
            };

            var meetUp2 = new MeetUp()
            {
                Id = 2,
                CostPerSeat = 0M,
                Location = "Location 2",
                Date = DateTime.Now.AddDays(32),
                SeatGrid = GenerateSeatGrid(10, 10)
            };

            appDbContext.MeetUps.AddRange(meetUp1, meetUp2);

            var booking1 = new Booking()
            {
                Id = 1,
                BookingEmail = "test@booking.com",
                Seats = new List<Seat>()
                {
                    new Seat { Id = 1, PersonName= "person1", ReferenceEmail = "person1@booking.com", SeatLabel="A1" },
                    new Seat { Id = 2, PersonName= "person2", ReferenceEmail = "person2@booking.com", SeatLabel="A2" },
                    new Seat { Id = 3, PersonName= "person3", ReferenceEmail = "person3@booking.com", SeatLabel="A3" },
                    new Seat { Id = 4, PersonName= "person4", ReferenceEmail = "person4@booking.com", SeatLabel="A1" },
                }
            };
            appDbContext.Bookings.Add(booking1);


            appDbContext.SaveChanges();
            //detach everything
            foreach (var entity in appDbContext.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }

        static SeatGrid GenerateSeatGrid(int seatRows, int seatPerRow)
        {
            string[] rowLabels = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z".Split(',');

            int seatNumStartAt = 1;
            SeatGrid newSeatGrid = new SeatGrid() { Id = seatGridId++ };

            for (int r = 0; r < seatRows; r++)
            {
                for (int s = seatNumStartAt; s < seatPerRow + 1; s++)
                {
                    newSeatGrid.Seats.Add(new Seat { Id = seatId++, SeatLabel = $"{rowLabels[r]}{s}" });
                }
            }
            return newSeatGrid;
        }
    }
}

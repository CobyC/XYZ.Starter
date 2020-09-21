using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;
using XYZ.Starter.Classes;

namespace XYZ.Starter.Data
{

    /// <summary>
    /// Application Context Using EF Core, this class is used for persisting the application entities to the data store
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<MeetUp> MeetUps { get; set; }
        public DbSet<SeatGrid> SeatGrids { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MeetUp>()
                .HasOne(mu=> mu.SeatGrid) //mu has a child (Seatgrid)
                .WithOne(sg=> sg.MeetUp) //sg has a parent (MeetUp)
                .HasForeignKey<SeatGrid>(sg=> sg.MeetUpId) //Seatgrid has a FK to MeetUp                
                .OnDelete(DeleteBehavior.Cascade); // if MU is deleted, delete SG and seats.

            modelBuilder.Entity<SeatGrid>()
                .HasMany(sg=> sg.Seats)
                .WithOne()
                .HasForeignKey(sgfk => sgfk.SeatGridId);

        }

    }
}

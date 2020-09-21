using System.Collections;
using System.Collections.Generic;
using XYZ.Starter.Core;

namespace XYZ.Starter.Classes
{
    /// <summary>
    /// Represents the seating grid
    /// </summary>
    public class SeatGrid : EntityBase
    {
        public SeatGrid()
        {
            Seats = new List<Seat>();
        }

        /// <summary>
        /// The list of seats for the grid.
        /// </summary>
        public IList<Seat> Seats { get; set; }

        public int MeetUpId { get; set; }

        public MeetUp MeetUp { get; set; }
    }
}

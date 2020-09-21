using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XYZ.Starter.Core;

namespace XYZ.Starter.Classes
{
    /// <summary>
    /// Represents the meet up
    /// </summary>
    public class MeetUp : EntityBase
    {
        public MeetUp()
        { }

        /// <summary>
        /// Get or Set the date of the meet up.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Get or Set where the meet up will take place
        /// </summary>
        [Required]
        public string Location { get; set; }

        /// <summary>
        /// Get or Set the cost per seat for this meet up
        /// </summary>
        public decimal? CostPerSeat { get; set; }

        /// <summary>
        /// Get or Set the seating grid owned by this meet up.
        /// </summary>
        public SeatGrid SeatGrid { get; set; }

    }
}

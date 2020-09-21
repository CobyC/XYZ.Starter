using System;
using System.ComponentModel.DataAnnotations;

namespace XYZ.Starter.Classes.Dtos
{

    /// <summary>
    /// Dto for requesting a new MeetUp
    /// </summary>
    public class CreateMeetUpDto
    {
        /// <summary>
        /// The date for the meet up
        /// </summary>
        [Required]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Location for the meet up
        /// </summary>
        [Required]
        public string Location { get; set; }

        /// <summary>
        /// number of rows that will have seats
        /// </summary>
        [Range(1, 10)]
        public int SeatRows { get; set; }

        /// <summary>
        /// number of seats in a row
        /// </summary>
        [Range(1, 10)]
        public int SeatsPerRow { get; set; }

        /// <summary>
        /// the cost per seat for the meet up
        /// </summary>        
        public decimal? CostPerSeat { get; set; }
    }

    /// <summary>
    /// Dto used for sending a list of MeetUp records without the grids, useful in showing a list of MeetUps
    /// </summary>
    public class MeetUpHeaderDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Get or Set the date of the meet up.
        /// </summary>        
        public DateTime Date { get; set; }

        /// <summary>
        /// Get or Set where the meet up will take place
        /// </summary>        
        public string Location { get; set; }

        /// <summary>
        /// Get or Set the cost per seat for this meet up
        /// </summary>
        public decimal? CostPerSeat { get; set; }
        
    }

    /// <summary>
    /// Represents the full meet up Dto used for over the wire transport
    /// </summary>
    public class MeetUpDto
    {

        public int Id { get; set; }
        /// <summary>
        /// Get or Set the date of the meet up.
        /// </summary>
        [Required]
        public DateTime? Date { get; set; }

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
        public SeatGridDto SeatGrid { get; set; }
    }
}

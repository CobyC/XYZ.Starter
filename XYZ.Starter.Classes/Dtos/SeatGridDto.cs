using System;
using System.Collections.Generic;
using System.Text;

namespace XYZ.Starter.Classes.Dtos
{
    public class SeatGridDto
    {
        public SeatGridDto()
        {
            Seats = new List<SeatDto>();
        }

        public int Id { get; set; }

        /// <summary>
        /// The list of seats for the grid.
        /// </summary>
        public IList<SeatDto> Seats { get; set; }

        public int MeetUpId { get; set; }
    }
}

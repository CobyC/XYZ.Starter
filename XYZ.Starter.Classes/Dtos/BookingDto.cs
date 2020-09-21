using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using XYZ.Starter.Core;

namespace XYZ.Starter.Classes.Dtos
{
    public class BookingDto
    {
        public BookingDto()
        { }
        public int Id { get; set; }

        public string BookingEmail { get; set; }
        

        public IList<SeatDto> Seats { get; set; }
    }
}

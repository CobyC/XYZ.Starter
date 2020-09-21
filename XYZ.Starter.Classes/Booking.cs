using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using XYZ.Starter.Core;

namespace XYZ.Starter.Classes
{
    public class Booking : EntityBase
    {
        public Booking()
        { }

        public string BookingEmail { get; set; }
        

        public IList<Seat> Seats { get; set; }
    }
}

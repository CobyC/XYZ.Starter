using XYZ.Starter.Core;

namespace XYZ.Starter.Classes
{
    public class Seat : EntityBase
    {
        public Seat()
        { }

        public string ReferenceEmail { get; set; }

        public string PersonName { get; set; }

        public string SeatLabel { get; set; }
        
        public int SeatGridId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using XYZ.Starter.Classes;

namespace XYZ.Starter.Data.Interfaces
{
    public interface IMeetUpManager
    {
        /// <summary>
        /// Generate a default meet up with the date as the last day of the month, 100 seats, and a default location
        /// </summary>
        /// <returns>The meet up with default values</returns>
        MeetUp CreateNewMeetUp();

        /// <summary>
        /// Generate a customised meet up with the date, location, seats and cost specified.
        /// </summary>
        /// <returns>The meet up with specified values</returns>
        MeetUp CreateNewMeetUp(DateTime meetUpDate, string meetUpLocation, int seatRows, int seatColumns, decimal cost);

    }
}

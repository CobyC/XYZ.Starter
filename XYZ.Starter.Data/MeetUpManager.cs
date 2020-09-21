using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XYZ.Starter.Classes;
using XYZ.Starter.Data.Interfaces;

namespace XYZ.Starter.Data
{
    /// <summary>
    /// Use this class to manage the way meet ups are created.
    /// </summary>
    public class MeetUpManager : IMeetUpManager
    {
        const string _meetUpLocation = "The Offices, Somewhere";
        readonly int _seatRows = 10;
        readonly int _seatPerRow = 10;
        readonly decimal _costPerSeat = 0M;
        const string _rowLabelcsv = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

        /// <summary>
        /// generate a default meet up with the date as the last day of the month, 100 seats, and a default location
        /// </summary>
        /// <returns>The meet up with default values</returns>
        public MeetUp CreateNewMeetUp()
        {
            MeetUp newMeetUp = new MeetUp()
            {
                CostPerSeat = _costPerSeat,
                Location = _meetUpLocation,
                Date = GetLastWorkDateOfMonth(),
                SeatGrid = GenerateSeatGrid(_seatRows, _seatPerRow)
            };
            return newMeetUp;
        }

        /// <summary>
        /// Generate a customised meet up with the date, location, seats and cost specified.
        /// </summary>
        /// <returns>The meet up with specified values</returns>
        public MeetUp CreateNewMeetUp(DateTime meetUpDate, string meetUpLocation, int seatRows, int seatsPerRow, decimal costPerSeat)
        {
            MeetUp newMeetUp = new MeetUp()
            {
                CostPerSeat = costPerSeat,
                Location = meetUpLocation,
                Date = meetUpDate,
                SeatGrid = GenerateSeatGrid(seatRows, seatsPerRow)
            };
            return newMeetUp;
        }

        DateTime GetLastWorkDateOfMonth()
        {
            int monthNumber = DateTime.Now.Month;
            int yearNumber = DateTime.Now.Year;
            int daysInMonth = DateTime.DaysInMonth(yearNumber, monthNumber);
            //calculate last working day of month
            //there was no requirement for this, nor is there requirements to restrict
            return new DateTime(yearNumber, monthNumber, daysInMonth);

        }

        /// <summary>
        /// Generate a blank seating grid with seat labels generated
        /// </summary>       
        /// <returns>a generated SeatGrid representing with labeled seats for the grid</returns>
        SeatGrid GenerateSeatGrid(int seatRows, int seatPerRow)
        {
            string[] rowLabels = _rowLabelcsv.Split(',');

            int seatNumStartAt = 1;
            SeatGrid newSeatGrid = new SeatGrid();
            for (int r = 0; r < seatRows; r++)
            {
                for (int s = seatNumStartAt; s < seatPerRow + 1; s++)
                {
                    newSeatGrid.Seats.Add(new Seat { SeatLabel = $"{rowLabels[r]}{s}" });
                }
            }
            return newSeatGrid;
        }
    }
}

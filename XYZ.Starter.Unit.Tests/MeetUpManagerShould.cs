using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XYZ.Starter.Classes;
using XYZ.Starter.Data;
using XYZ.Starter.Data.Interfaces;

namespace XYZ.Starter.Unit.Tests
{
    public class MeetUpManagerShould
    {


        //In our current meetup space we have 100 seats available, 10 rows of 10 seats, 
        //labeled A1 to J10, although in the future this may change. 

        [Fact]
        public void Set_Up_With_Defaults_For_New_MeetUp_By_Constructor()
        {
            //Arrange
            IMeetUpManager manager = new MeetUpManager();
            DateTime lastDate = new DateTime(2020, DateTime.Now.Month, DateTime.DaysInMonth(2020, DateTime.Now.Month));

            //Act 
            MeetUp meetUp = manager.CreateNewMeetUp();

            //Assert            
            Assert.Equal("The Offices, Somewhere", meetUp.Location);
            Assert.Equal(0M, meetUp.CostPerSeat);
            Assert.Equal(lastDate, meetUp.Date);
        }

        [Fact]
        public void Set_Up_With_Constructor_Values_For_New_MeetUp()
        {
            //Arrange
            IMeetUpManager manager = new MeetUpManager();
            DateTime lastDate = DateTime.Now.AddDays(5);

            //Act 
            MeetUp meetUp = manager.CreateNewMeetUp(lastDate, "London", 5, 5, 10);

            //Assert            
            Assert.Equal("London", meetUp.Location);
            Assert.Equal(10M, meetUp.CostPerSeat);
            Assert.Equal(lastDate, meetUp.Date);
            Assert.Equal(25, meetUp.SeatGrid.Seats.Count);
        }

        [Fact]
        public void Set_Up_Default_Grid_With_100_Seats_For_New_MeetUp()
        {
            //Arrange
            IMeetUpManager manager = new MeetUpManager();

            //Act 
            MeetUp meetUp = manager.CreateNewMeetUp();

            //Assert
            Assert.True(meetUp.SeatGrid != null);
            Assert.Equal(100, meetUp.SeatGrid.Seats.Count);

        }

        [Fact]
        public void Set_Up_Default_Grid_With_A1_J10_Labels()
        {
            //Arrange
            IMeetUpManager manager = new MeetUpManager();

            //Act 
            MeetUp meetUp = manager.CreateNewMeetUp();

            //Assert
            Assert.Equal(100, meetUp.SeatGrid.Seats.Count);
            Assert.Equal("A1", meetUp.SeatGrid.Seats[0].SeatLabel);
            Assert.Equal("J10", meetUp.SeatGrid.Seats[99].SeatLabel);

        }
    }
}

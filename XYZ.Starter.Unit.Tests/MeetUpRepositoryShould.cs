using ServiceStack;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using XYZ.Starter.Classes;
using XYZ.Starter.Classes.Dtos;
using XYZ.Starter.Data;
using XYZ.Starter.Data.Interfaces;

namespace XYZ.Starter.Unit.Tests
{
    public class MeetUpRepositoryShould
    {

        [Fact]
        public async Task Get_a_record_lazy_when_using_find_by_id_and_not_lazy_when_fetch_by_id()
        {
            //arrange
            AppDbContext appDbContext = AppDbContextMocker.GetAppDbContext("test_1");
            IMeetUpRepository _repo = new MeetUpRepository(appDbContext);

            //act
            var ent = await _repo.FindByIdAsync(1);
            var dto = ent.ConvertTo<MeetUpHeaderDto>();

            //assert
            Assert.True(ent.SeatGrid is null);
            Assert.True(dto.Id == 1);


            //act
            ent = await _repo.FetchByIdAsync(1);
            var dtof = ent.ConvertTo<MeetUpDto>();

            //assert
            Assert.True(ent.SeatGrid?.Seats != null);
            Assert.True(dtof.SeatGrid?.Seats != null);
            Assert.True(ent.SeatGrid?.Seats[0].SeatLabel == "A1");
            Assert.True(ent.SeatGrid?.Seats[99]?.SeatLabel == "J10");

            //clean up otherwise the other test will complain about key tracking.
            await appDbContext.DisposeAsync();
        }

        [Fact]
        public async Task Create_new_MeetUp_from_new_meetupDto_has_ids_and_same_values()
        {
            //arrange
            AppDbContext appDbContext = AppDbContextMocker.GetAppDbContext("test_2");
            IMeetUpRepository _repo = new MeetUpRepository(appDbContext);
            IMeetUpManager meetUpManager = new MeetUpManager();
            
            CreateMeetUpDto newM = new CreateMeetUpDto()
            {
                Date = DateTime.Now,
                CostPerSeat = 0M,
                Location = "New Location",
                SeatRows = 10,
                SeatsPerRow = 10
            };

            MeetUp mu = meetUpManager.CreateNewMeetUp(newM.Date.Value, newM.Location, newM.SeatRows, newM.SeatsPerRow, newM.CostPerSeat.Value);

            //act
            mu = _repo.Create(mu);
            await _repo.SaveChangesAsync();

            //assert
            Assert.True(mu.Id == 3);
            Assert.Equal(newM.Location, mu.Location);
            Assert.Equal(3, mu.SeatGrid.Id);
            Assert.Equal(100, mu.SeatGrid.Seats?.Count);

            //clean up otherwise the other test will complain about key tracking.
            await appDbContext.DisposeAsync();
        }


        [Fact]
        public async Task Updating_meetup_from_untracked_should_update()
        {
            //arrange
            MeetUp meetUp = new MeetUp()
            {
                Id = 1,
                CostPerSeat = 1M,
                Location = "Updated Location",
                Date = DateTime.Now.AddDays(2)
            };
            AppDbContext appDbContext = AppDbContextMocker.GetAppDbContext("test_3");
            IMeetUpRepository _repo = new MeetUpRepository(appDbContext);

            //act
            _repo.Update(meetUp);
            await _repo.SaveChangesAsync();

            var getEnt = _repo.FetchById(meetUp.Id);

            //assert
            Assert.Equal(meetUp.Id, getEnt.Id);
            Assert.Equal(meetUp.Location, getEnt.Location);//default location is Location 1
            Assert.True(getEnt.SeatGrid != null);


            await appDbContext.DisposeAsync();
        }


        [Fact]
        public async Task Removing_record_should_remove_all_children()
        {
            //arrange
            MeetUp meetUp = new MeetUp()
            {
                Id = 1,
                CostPerSeat = 0M,
                Location = "Delete Record",
                Date = DateTime.Now.AddDays(2)
            };
            AppDbContext appDbContext = AppDbContextMocker.GetAppDbContext("test_4");
            IMeetUpRepository _repo = new MeetUpRepository(appDbContext);

            //act
            _repo.Delete(meetUp); // delete by entity
            await _repo.SaveChangesAsync();
            var delEnt = await _repo.FindByIdAsync(meetUp.Id);

            _repo.Delete(2);
            await _repo.SaveChangesAsync();
            var delEnt2 = await _repo.FindByIdAsync(2);

            var all = await _repo.FindByExpressionAsync(m => m.Id > 0);

            var anyGrids = appDbContext.SeatGrids.Where(x => x.Id > 0);
            //assert
            Assert.Null(delEnt);
            Assert.Null(delEnt2);
            Assert.True(all.Count() == 0);
            Assert.True(anyGrids.Count() == 0);

            await appDbContext.DisposeAsync();
        }
    }
}

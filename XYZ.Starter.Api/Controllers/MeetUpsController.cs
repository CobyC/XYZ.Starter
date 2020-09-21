using ApiXYZ.Starter.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using XYZ.Starter.Classes.Dtos;
using XYZ.Starter.Core;
using XYZ.Starter.Data.Interfaces;

namespace XYZ.Starter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeetUpsController : ControllerBase
    {

        private readonly ILogger<MeetUpsController> _logger;
        readonly IMeetUpManager _meetUpManager;
        readonly IMeetUpRepository _repository;

        public MeetUpsController(ILogger<MeetUpsController> logger, IMeetUpRepository repository, IMeetUpManager meetUpManager)
        {
            this._repository = repository;
            this._meetUpManager = meetUpManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetUpHeaderDto>>> GetAll()
        {
            var meetUpList = await _repository.FindByExpressionAsync(f => f.Id > 0);
            return meetUpList.ConvertAllTo<MeetUpHeaderDto>().ToList();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MeetUpDto>> GetById(int id)
        {
            if (!await _repository.DoesExistAsync(id))
                return NotFound();

            var entity = await _repository.FetchByIdAsync(id);

            await Task.CompletedTask;
            return Ok(entity.ConvertTo<MeetUpDto>());
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelStateActionFilter))]
        public async Task<ActionResult<MeetUpDto>> CreateNew(CreateMeetUpDto newMeetUp)
        {
            if (ModelState.IsValid)
            {
                var addMeetUp = _meetUpManager.CreateNewMeetUp(newMeetUp.Date.Value, newMeetUp.Location, newMeetUp.SeatRows, newMeetUp.SeatsPerRow, newMeetUp.CostPerSeat.Value);

                await _repository.CreateAsync(addMeetUp);
                await _repository.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), addMeetUp.Id, addMeetUp.ConvertTo<MeetUpDto>());
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidateModelStateActionFilter))]
        public async Task<ActionResult> UpdateMeetUp(int id, MeetUpDto updMeetUp)
        {
            if (!await _repository.DoesExistAsync(id))
                return NotFound();

            var updEntity = await _repository.FindByIdAsync(id);
            updEntity.PopulateFromPropertiesWithoutAttribute(updMeetUp, typeof(KeyAttribute));

            await _repository.SaveChangesAsync();

            return NoContent();
        }
        //[HttpPatch]


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeetUp(int id)
        {
            if (!await _repository.DoesExistAsync(id))
                return NotFound();
                       
            _repository.Delete(id);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}

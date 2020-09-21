using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XYZ.Starter.Classes;
using XYZ.Starter.Classes.Dtos;
using XYZ.Starter.Data;
using XYZ.Starter.Core;

namespace ApiXYZ.Starter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var list = await _context.Bookings.ToListAsync();
            return list.ConvertAllTo<BookingDto>().ToList();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int id)
        {
            var Booking = await _context.Bookings.FindAsync(id);

            if (Booking == null)
            {
                return NotFound();
            }

            return Booking.ConvertTo<BookingDto>();
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, BookingDto Booking)
        {
            if (id != Booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(Booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BookingDto>> PostBooking(BookingDto Booking)
        {
            _context.Bookings.Add(Booking.ConvertTo<Booking>());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = Booking.Id }, Booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookingDto>> DeleteBooking(int id)
        {
            var Booking = await _context.Bookings.FindAsync(id);
            if (Booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(Booking);
            await _context.SaveChangesAsync();

            return Booking.ConvertTo<BookingDto>();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}

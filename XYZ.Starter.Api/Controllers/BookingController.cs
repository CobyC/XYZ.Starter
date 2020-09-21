using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XYZ.Starter.Data;

namespace ApiXYZ.Starter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        readonly AppDbContext _appDbContext;

        public BookingController(ILogger<BookingController> logger, AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            _logger = logger;
        }
    }
}

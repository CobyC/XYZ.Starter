using ApiXYZ.Starter.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XYZ.Starter.Data;

namespace XYZ.Starter.Unit.Tests
{
    public class BookingControllerShould
    {
        private BookingController InitializeController(AppDbContext appDbContext)
        {
            var _controller = new BookingController(null, appDbContext);

            var mockHttpContext = new Mock<HttpContext>();

            var context = new ControllerContext(new ActionContext(mockHttpContext.Object, new RouteData(), new ControllerActionDescriptor()));
            _controller.ControllerContext = context;

            return _controller;
        }

        [Fact]
        public async Task Get_All_Booking_Records()
        {
            // Arrange
            var _AppDbContext = AppDbContextMocker.GetAppDbContext(nameof(Get_All_Booking_Records));
            var _controller = InitializeController(_AppDbContext);

            //Act
            

            //Assert
            

            //clean up otherwise the other test will complain about key tracking.
            await _AppDbContext.DisposeAsync();
        }
    }
}

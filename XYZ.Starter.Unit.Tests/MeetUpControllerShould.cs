using ApiXYZ.Starter.Api.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using XYZ.Starter.Api.Controllers;
using XYZ.Starter.Classes.Dtos;
using XYZ.Starter.Data;

namespace XYZ.Starter.Unit.Tests
{
    public class MeetUpControllerShould
    {
        private MeetUpsController InitializeController(AppDbContext appDbContext)
        {
            var _controller = new MeetUpsController(null, new MeetUpRepository(appDbContext), new MeetUpManager());

            var spf = new DefaultServiceProviderFactory(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
            var sc = spf.CreateBuilder(new ServiceCollection());

            sc.AddMvc();
            sc.AddControllers();
            //(config =>
            //{
            //    config.Filters.Add(new ValidateModelStateAttribute());
            //    config.Filters.Add(new ValidateEntityExistAttribute<IEntityBase>(appDbContext));
            //});

            sc.AddTransient<ValidateModelStateActionFilter>();

            var sp = sc.BuildServiceProvider();
            //var mockHttpContext = new Mock<HttpContext>();
            var httpContext = new DefaultHttpContext
            {
                RequestServices = sp
            };
            //mockHttpContext.Setup(cx => cx.RequestServices).Returns(sp);


            //var contDesc = new ControllerActionDescriptor() {  };

            //var context = new ControllerContext();// new ActionContext(httpContext, new RouteData(), contDesc));
            var context = new ControllerContext(new ActionContext(httpContext, new RouteData(), new ActionDescriptor()));

            //context.RouteData = new RouteData() { };
            //context.HttpContext = mockHttpContext.Object;
            //context.HttpContext = httpContext;
            //_controller.ControllerContext.HttpContext = httpContext;
            _controller.ControllerContext = context;

            return _controller;
        }

        [Fact]
        public async Task Get_All_MeetUp_Records()
        {
            // Arrange
            var _AppDbContext = AppDbContextMocker.GetAppDbContext(nameof(Get_All_MeetUp_Records));
            var _controller = InitializeController(_AppDbContext);

            //Act
            var all = await _controller.GetAll();

            //Assert
            Assert.True(all.Value.Count() > 0);

            //clean up otherwise the other test will complain about key tracking.
            await _AppDbContext.DisposeAsync();
        }

        [Fact]
        public async Task Get_MeetUp_Record_By_Id()
        {
            // Arrange
            var _AppDbContext = AppDbContextMocker.GetAppDbContext(nameof(Get_MeetUp_Record_By_Id));
            var _controller = InitializeController(_AppDbContext);

            //Act          

            var ent = await _controller.GetById(1);

            //Assert
            Assert.True(ent.Result is OkObjectResult);
            var resVal = (ent.Result as OkObjectResult).Value;
            Assert.True(resVal is MeetUpDto);
            var dto = resVal as MeetUpDto;
            Assert.Equal(1, dto.Id);

            //clean up otherwise the other test will complain about key tracking.
            await _AppDbContext.DisposeAsync();
        }

        [Fact]
        public async Task Test_the_Action_Filter()
        {
            //Arrange

            //act

            //assert

            await Task.CompletedTask;

        }

    }
}

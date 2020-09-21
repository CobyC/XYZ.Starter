using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using XYZ.Starter.Classes.Dtos;
using XYZ.Starter.Core;

namespace XYZ.Starter.Integration.Tests
{
    public class MeetUpsControllerShould : IClassFixture<TestFixture<XYZ.Starter.Api.Startup>>
    {
        private HttpClient _HttpClient;
        private const string _BaseRequestUri = "/api/meetups";

        public MeetUpsControllerShould(TestFixture<XYZ.Starter.Api.Startup> fixture)
        {
            _HttpClient = fixture.HttpClient;
            fixture.SeedDataToContext();
        }

        [Fact]
        public async Task Get_All_MeetUps()
        {
            //arrange
            var request = _BaseRequestUri;

            //act
            var response = await _HttpClient.GetAsync(request);

            //assert
            response.EnsureSuccessStatusCode(); //if exception is not thrown all is good

            //convert the response content to expected result and test response
            var result = await ContentHelper.ContentTo<IEnumerable<MeetUpHeaderDto>>(response.Content);
            Assert.NotNull(result);

        }

        [Fact]
        public async Task Get_MeetUp_By_Id()
        {
            //arrange
            var request = $"{_BaseRequestUri}/1";

            //act
            var response = await _HttpClient.GetAsync(request);

            //assert
            response.EnsureSuccessStatusCode(); //if exception is not thrown all is good
            //convert the response content to expected result and test response
            var result = await ContentHelper.ContentTo<MeetUpDto>(response.Content);
            Assert.NotNull(result);

        }
    }
}

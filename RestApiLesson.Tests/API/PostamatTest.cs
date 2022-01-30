using RestApiLesson.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestApiLesson.Tests.API
{
    public class PostamatTest : IClassFixture<BaseTestServerFixture>
    {
        private readonly BaseTestServerFixture _fixture;
        public PostamatTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task PostamatGetAllTestAsync()
        {
            // Arrange
            // Act
            var responce = await _fixture.Client.GetAsync("api/postamat/all");
            responce.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
        }

        [Fact]
        public async Task PostamatGetInfoTestAsync()
        {
            // Arrange
            var postamat = new Postamat()
            {
                Number = "0000-0000",
                Adress = "Adress1",
                IsWorking = true
            };

            _fixture.PostamatsContext.Postamats.Add(postamat);
            _fixture.PostamatsContext.SaveChanges();

            // Act
            var responce = await _fixture.Client.GetAsync("api/postamat/0000-0000");
            responce.EnsureSuccessStatusCode();
            // Assert
            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Postamat>(await responce.Content.ReadAsStringAsync());
            Assert.Equal("0000-0000", result.Number);
        }

        [Fact]
        public async Task PostamatNotMatchNumberReturnsBadRequestTest()
        {
            // Arrange
            // Act
            var responce = await _fixture.Client.GetAsync("api/postamat/badname");
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
        }
    }
}

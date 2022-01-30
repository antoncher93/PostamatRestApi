using Newtonsoft.Json;
using RestApiLesson.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestApiLesson.Tests.API
{
    public class OrderTest : IClassFixture<BaseTestServerFixture>
    {
        private readonly BaseTestServerFixture _fixture;
        public OrderTest(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task OrderMoreTenProductsReturnBadRequestTest()
        {
            var order = new Order()
            {
                Price = 750,
                Products = Enumerable.Range(1, 11).Select(i => $"Product{i}").ToArray(),
                TelephoneNumber = "+7123-456-78-90"
            };

            var postamatNumber = "abcd-abcd";

            var postamat = new Postamat()
            {
                Adress = "Ленина 20",
                IsWorking = true, 
                Number = postamatNumber
            };

            _fixture.PostamatsContext.Add(postamat);
            _fixture.PostamatsContext.SaveChanges();

            var body = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");

            // Action
            var responce = await _fixture.Client.PostAsync($"api/order/new/{postamatNumber}", body);
            
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responce.StatusCode);
        }

        [Fact]
        public async Task OrderToNotWorkingPostamatReturnsForbidenTest()
        {
            // Arrange
            var order = new Order()
            {
                Price = 750,
                Products = new[] { "Product1", "Product2" },
                TelephoneNumber = "+7123-456-78-90"
            };

            var postamatNumber = "aaaa-bbbb";

            var postamat = new Postamat()
            {
                Adress = "Ленина 15",
                IsWorking = false, // постамат не работает
                Number = postamatNumber
            };

            _fixture.PostamatsContext.Add(postamat);
            _fixture.PostamatsContext.SaveChanges();

            var body = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");

            // Action
            var responce = await _fixture.Client.PostAsync($"api/order/new/{postamatNumber}", body);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, responce.StatusCode);
        }
    }
}

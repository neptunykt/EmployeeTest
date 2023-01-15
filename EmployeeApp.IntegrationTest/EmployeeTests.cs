using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Core.Entities;
using Newtonsoft.Json;
using WebApp;
using Xunit;

namespace EmployeeApp.IntegrationTest
{
    public class EmployeeTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public EmployeeTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task CreateEmployeeTestAsync()
        {
            // Arrange
            var httpClient =_factory.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:5000");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            var fixture = new Fixture();
            var employee = fixture.Build<Employee>()
                .With(o => o.Surname, "Petrov")
                .With(o=>o.Forenames, "Peter")
                .With(o=>o.Mobile, "12345678")
                .With(o=>o.EmailHome, "peter@mail.ru")
                .With(o=>o.Telephone,"123455423")
                .With(o=>o.PayRollNumber, "12G344")
                .Create();
            using var response = await httpClient.PostAsync("api/Employee",
                new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json"));
            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
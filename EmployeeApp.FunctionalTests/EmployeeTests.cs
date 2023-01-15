using System.Threading.Tasks;
using AutoFixture;
using Core.DAL;
using Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using WebApp;
using Xunit;

namespace EmployeeApp.FunctionalTests
{
    public class EmployeeTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public EmployeeTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task CreateEmployeeTestAsync()
        {
            // Arrange
            var fixture = new Fixture();

            var employee = fixture.Build<Employee>()
                .With(p=>p.Surname, "Petrov")
                .With(p=>p.Forenames, "Peter")
                .Create();
            
            // Act
            var serviceScope = _factory.Server.Host.Services.CreateScope();
            var employeeRepository = serviceScope.ServiceProvider.GetRequiredService<IEmployeeRepository>();
            var savedEmployee = await employeeRepository.AddEmployeeAsync(employee);
            var fromDatabaseEmployee = await employeeRepository.GetEmployeeAsync(savedEmployee.Id);
            
            // Assert
            Assert.Equal(savedEmployee.Forenames, fromDatabaseEmployee.Forenames);
            Assert.Equal(savedEmployee.Surname, fromDatabaseEmployee.Surname);

        }
        
        [Fact]
        public async Task UpdateEmployeeTestAsync()
        {
            // Arrange
            var fixture = new Fixture();

            var employee = fixture.Build<Employee>()
                .With(p=>p.Surname, "Petrov")
                .With(p=>p.Forenames, "Peter")
                .Create();
            
            // Act
            var serviceScope = _factory.Server.Host.Services.CreateScope();
            var employeeRepository = serviceScope.ServiceProvider.GetRequiredService<IEmployeeRepository>();
            var savedEmployee = await employeeRepository.AddEmployeeAsync(employee);
            employee.Surname = "Ivanov";
            employee.Forenames = "Ivan";
            var editEmployee = await employeeRepository.UpdateEmployeeAsync(employee);
            
            // Assert
            Assert.Equal("Ivan", editEmployee.Forenames);
            Assert.Equal("Ivanov", editEmployee.Surname);

        }
    }
}
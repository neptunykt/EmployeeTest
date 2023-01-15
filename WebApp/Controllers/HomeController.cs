using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Model;
using Core.Requests;
using Core.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApp.Infrastructure;

namespace WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/Employee")]
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    public class HomeController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoadService _loadService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="loadService"></param>
        public HomeController(IMediator mediator, ILoadService loadService)
        {
            _mediator = mediator;
            _loadService = loadService;
        }
        
        /// <summary>
        /// Get Employee by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation("GetEmployee")]
        public async Task<Employee> GetEmployeeAsync(Guid id) =>
            await _mediator.Send(new GetEmployee{Id =id});
        
        /// <summary>
        /// Create Employee
        /// </summary>
        /// <param name="createEmployee"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("CreateEmployee")]
        public async Task<Employee> CreateEmployeeAsync(
            [FromBody] CreateEmployee createEmployee) => await _mediator.Send(createEmployee);

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="updateEmployee"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerOperation("UpdateEmployee")]
        public async Task<Employee> UpdateEmployeeAsync(
            [FromBody] UpdateEmployee updateEmployee) => await _mediator.Send(updateEmployee);

        /// <summary>
        /// Create Employees
        /// </summary>
        /// <param name="createEmployees"></param>
        /// <returns></returns>
        [HttpPost("CreateEmployees")]
        [SwaggerOperation("CreateEmployees")]
        public async Task<IEnumerable<Employee>> CreateEmployeesAsync(
            [FromBody] IEnumerable<CreateEmployee> createEmployees)
        {
            var employees = new List<Employee>();
            foreach (var createEmployee in createEmployees)
            {
               var employee = await _mediator.Send(createEmployee);
               employees.Add(employee);
            }

            return employees;
        }
        /// <summary>
        /// Upload csv file
        /// </summary>
        /// <param name="upload"></param>
        [HttpPost("LoadCsv")]
        [SwaggerOperation("UploadCsv")]
        public async Task<LoadResult> UploadCsvAsync(IFormFile upload) =>
            await _loadService.ReadAsync(upload);

        /// <summary>
        /// Get Employees page
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDirection"></param>
        /// <param name="searchText"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("page/{pageIndex}")]
        [SwaggerOperation("GetEmployeesPage")]
        public async Task<PaginableContentModel<Employee>> GetEmployeesPageAsync(int pageIndex, int pageSize = 30,
            string orderBy = "", OrderDirection orderDirection = OrderDirection.Ascending, string searchText = null, string searchDate = null)
        {
            return await _mediator.Send(new GetEmployeesPage(pageIndex, pageSize, orderBy, orderDirection, searchText, searchDate));
        }
        
    }
}
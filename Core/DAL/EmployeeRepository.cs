using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _db;
        
        public EmployeeRepository(DataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee</returns>
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
           var employeeAdded = _db.Employees.Add(employee);
           await  _db.SaveChangesAsync();
           return employeeAdded.Entity;
        }


        /// <summary>
        /// Get Employees Page by page index, page size, order by, order direction
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDirection"></param>
        /// <param name="searchText"></param>
        /// <param name="searchDate"></param>
        /// <returns>Pageable model</returns>
        public async Task<PaginableContentModel<Employee>> GetEmployeesPageAsync(int pageIndex, int pageSize, string orderBy,
            OrderDirection? orderDirection, string searchText, string searchDate)
        {

            var query = _db.Employees.AsQueryable();
            var employees = query
                .OrderBy(orderBy, orderDirection)
                .SearchText(searchText)
                .SearchDate(searchDate)
                .Skip(pageIndex * pageSize).Take(pageSize);
            // count request
            var totalCount = await _db.Employees.AsQueryable().SearchText(searchText).CountAsync();
            var result = new PaginableContentModel<Employee>();
            result.TotalItems = totalCount;
            result.PageItems = await employees.ToListAsync();
            result.CurrentPageIndex = pageIndex;
            return result;

        }

        /// <summary>
        /// Get Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ServiceException"></exception>
        ///  <returns>Employee</returns>
        public async Task<Employee> GetEmployeeAsync(Guid id)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(p => p.Id == id);
            if (employee == null)
            {
                throw new ServiceException("NOT_FOUND");
            }

            return employee;
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
           var employeeEntry =  _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
            return employeeEntry.Entity;
        }
        
    }
}
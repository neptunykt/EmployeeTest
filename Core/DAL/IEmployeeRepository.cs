using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Model;

namespace Core.DAL
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployeeAsync(Employee employee);

        Task<PaginableContentModel<Employee>> GetEmployeesPageAsync(int pageIndex, int pageSize, string orderBy,
            OrderDirection? orderDirection, string searchText, string searchDate);

        Task<Employee> GetEmployeeAsync(Guid id);

        Task<Employee> UpdateEmployeeAsync(Employee employee);
        
    }
}
using System.Threading;
using System.Threading.Tasks;
using Core.DAL;
using Core.Entities;
using Core.Model;
using MediatR;

namespace Core.Requests
{
    public class GetEmployeesPageHandler : IRequestHandler<GetEmployeesPage, PaginableContentModel<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeesPageHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<PaginableContentModel<Employee>> Handle(GetEmployeesPage request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetEmployeesPageAsync(request.PageIndex, request.PageSize, request.OrderBy,
                request.OrderDirection, request.SearchText, request.SearchDate);
        }
    }
}
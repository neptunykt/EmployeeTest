using System.Threading;
using System.Threading.Tasks;
using Core.DAL;
using Core.Entities;
using MediatR;

namespace Core.Requests
{
    public class GetEmployeeHandler : IRequestHandler<GetEmployee, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(GetEmployee request, CancellationToken cancellationToken) =>
            await _employeeRepository.GetEmployeeAsync(request.Id);
    }
}
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL;
using Core.Entities;
using MediatR;

namespace Core.Requests
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployee, Employee>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        public CreateEmployeeHandler(
             IMapper mapper, 
            IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(CreateEmployee request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.AddEmployeeAsync(_mapper.Map<CreateEmployee, Employee>(request));
            return employee;
        }
    }
}
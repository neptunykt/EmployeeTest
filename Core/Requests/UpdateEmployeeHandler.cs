using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL;
using Core.Entities;
using MediatR;

namespace Core.Requests
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<Employee> Handle(UpdateEmployee request, CancellationToken cancellationToken)
        {
          return await _employeeRepository.UpdateEmployeeAsync(_mapper.Map<UpdateEmployee, Employee>(request));
        }
    }
}
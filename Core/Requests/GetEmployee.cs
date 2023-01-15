using System;
using Core.Entities;
using MediatR;

namespace Core.Requests
{
    public class GetEmployee : IRequest<Employee>
    {
        public Guid Id { get; set; }
    }
}
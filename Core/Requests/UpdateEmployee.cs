using System;
using Core.Entities;
using MediatR;

namespace Core.Requests
{
    public class UpdateEmployee : IRequest<Employee>
    {
        public Guid Id { get; set; }
        public string PayRollNumber { get; set; }

            public string Surname { get; set; }

            public string Forenames { get; set; }

            public DateTime DateOfBirth { get; set; }


            public string Telephone { get; set; }


            public string Mobile { get; set; }


            public string Address { get; set; }


            public string Address2 { get; set; }


            public string PostCode { get; set; }


            public string EmailHome { get; set; }
            public DateTime StartDate { get; set; }
    }
}
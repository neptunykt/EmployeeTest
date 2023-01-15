using FluentValidation;

namespace Core.Requests
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployee>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.PayRollNumber).NotEmpty().MaximumLength(6);

            RuleFor(x => x.Surname).NotEmpty().MaximumLength(255);

            RuleFor(x => x.Forenames).NotEmpty().MaximumLength(255);

            RuleFor(x => x.DateOfBirth).NotEmpty();

            RuleFor(x => x.Telephone).NotEmpty().MaximumLength(40);
            
            RuleFor(x => x.Mobile).NotEmpty().MaximumLength(40);
            
            RuleFor(x => x.Address).NotEmpty().MaximumLength(255);
            
            RuleFor(x => x.Address2).NotEmpty().MaximumLength(255);
            
            RuleFor(x => x.PostCode).NotEmpty().MaximumLength(40);
            
            RuleFor(x => x.EmailHome).NotEmpty().EmailAddress().MaximumLength(255);
            
            RuleFor(x => x.StartDate).NotEmpty();
        }
    }
}
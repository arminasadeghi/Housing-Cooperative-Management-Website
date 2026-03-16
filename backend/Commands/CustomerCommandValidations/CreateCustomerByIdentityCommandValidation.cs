using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using housingCooperative.Commands.CustomerCommands;

namespace CustomerApi.Commands.Customer.CustomerCommandValidation
{
    public class CreateCustomerByIdentityCommandValidation : AbstractValidator<CreateCustomerByIdentityCommand>
    {

       public CreateCustomerByIdentityCommandValidation()
       {

           RuleFor(x=>x.CustomerId)
                .NotNull().WithMessage(Errors.RequiredItemIsNull.GetMessage())
                .NotEmpty().WithMessage(Errors.RequiredItemIsNull.GetMessage());

           RuleFor(x=>x.PhoneNumber)
                .NotNull().WithMessage(Errors.RequiredItemIsNull.GetMessage())
                .NotEmpty().WithMessage(Errors.RequiredItemIsNull.GetMessage());


       } 

       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using housingCooperative.Commands.CustomerCommands;

namespace housingCooperative.Commands.CustomerCommandValidation
{
    public class UpdateCustomerByUserCommandValidation : AbstractValidator<UpdateCustomerByUserCommand>
    {


       public UpdateCustomerByUserCommandValidation()
       {

            RuleFor(x=>x.CustomerId)
                .NotNull().WithMessage(Errors.RequiredItemIsNull.GetMessage())
                .NotEmpty().WithMessage(Errors.RequiredItemIsNull.GetMessage());



            RuleFor(x=>x.NationalId)
                .Must(x=>x.IsNationalIdValid()).WithErrorCode(Errors.InvalidNationalCode.GetCode().ToString())
                .NotNull().WithMessage(Errors.RequiredItemIsNull.GetMessage())
                .NotEmpty().WithMessage(Errors.RequiredItemIsNull.GetMessage())
                .Unless(x => x.NationalId is null);
        

            RuleFor(x=>x.BirthDate)
                .Must(x => {return  x <= DateTime.Now.Date ; }).WithErrorCode(Errors.InvalidBirthDate.GetCode().ToString())
                .Unless(x => x.BirthDate is null);
        } 
    }
}
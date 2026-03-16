using housingCooperative.Domains.Entities;
using housingCooperative.Enums;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Commands.CustomerCommands
{
    public class UpdateCustomerByUserCommand : CommandBase<ServiceResult<string>>
    {
        public string? CustomerId { get ;  set ; }
        public string FirstName { get ;  set ; }
        public string LastName { get ;  set ; }
        public string? NationalId { get ;  set ; }
        public DateTime? BirthDate { get ;  set ; }
        public GenderEnum? Gender { get ; set ;} 
        public string? JobTitle { get;  set; }
        public string? City { get;  set; }
        public string? Address { get;  set; }

        public UpdateCustomerByUserCommand(string customerId,string firstName, string lastName,  string? nationalId,
         DateTime? birthDate, GenderEnum? gender, string? jobTitle, string? city, string? address)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            NationalId = nationalId;
            BirthDate = birthDate;
            Gender = gender;
            JobTitle = jobTitle;
            City = city;
            Address = address;
        }

        public UpdateCustomerByUserCommand()
        {
        }

        public class UpdateCustomerByUserCommandHandler : BaseRequestCommandHandler<UpdateCustomerByUserCommand, ServiceResult<string>>
        {
            private readonly IUnitOfWork<housingCooperativeDBContext> unitOfWork;
            public UpdateCustomerByUserCommandHandler(ILogger<UpdateCustomerByUserCommand> logger, IUnitOfWork<housingCooperativeDBContext> _unitOfWork) : base(logger)
            {
                unitOfWork = _unitOfWork;
            }

            protected override async Task<ServiceResult<string>> HandleAsync(UpdateCustomerByUserCommand command, CancellationToken cancellationToken)
            {


                var sr = ServiceResult.Empty;
                var customerRepo = unitOfWork.GetRepository<CustomerEntity>();




                // Getting Customer Entity
                var customer = customerRepo.GetFirstOrDefault(predicate:x=>x.Id == command.CustomerId);



                if(customer==null)
                    return sr.SetError(Errors.CustomerNotFound.GetMessage() , Errors.CustomerNotFound.GetCode())
                    .SetMessage(Errors.CustomerNotFound.GetPersionMessage())
                    .To<string>();




                
                // Updating Customer
                customer.Update(command.FirstName,command.LastName,command.NationalId,command.BirthDate,command.Gender,
                command.JobTitle,command.City,command.Address);
                customerRepo.Update(customer);



                
                //Saving To DataBase
                unitOfWork.SaveChanges();

                return ServiceResult.Create<string>(customer.Id);


            }

            protected override ServiceResult<string> HandleOnError(System.Exception exp)
            {

                var sr = ServiceResult.Empty;
                sr = sr.SetError("UnHandled");

                sr.Error.ExteraFields = new KeyValueList<string, string>(){
                    new KeyValuePair<string, string>(nameof(exp.Message),exp.Message),
                    new KeyValuePair<string, string>(nameof(exp.InnerException),exp.InnerException?.Message ?? ""),
                };

                return sr.To<string>();

            }

            
        }
    }
}
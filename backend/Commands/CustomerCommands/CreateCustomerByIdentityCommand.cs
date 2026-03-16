using housingCooperative.Domains.Entities;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Commands.CustomerCommands
{
    public class CreateCustomerByIdentityCommand : CommandBase<ServiceResult<string>>
    {
        
        public CreateCustomerByIdentityCommand(string customerId, string phoneNumber)
        {
            CustomerId = customerId;
            PhoneNumber = phoneNumber;
        }

        public string CustomerId { get; set; }
        public string PhoneNumber { get; set; }

    }



    public class CreateCustomerByIdentityCommandHandler
        : BaseRequestCommandHandler<CreateCustomerByIdentityCommand, ServiceResult<string>>
    {


        private readonly IUnitOfWork<housingCooperativeDBContext> _unitOfWork;


        public CreateCustomerByIdentityCommandHandler(Microsoft.Extensions.Logging.ILogger<CreateCustomerByIdentityCommandHandler> logger, IUnitOfWork<housingCooperativeDBContext> unitOfWork) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }


        protected async override Task<ServiceResult<string>> HandleAsync(CreateCustomerByIdentityCommand command, CancellationToken cancellationToken)
        {




            var sr = ServiceResult.Empty ; 
            var customerRepo = _unitOfWork.GetRepository<CustomerEntity>();


            

            // Get CustomerEntity
            var Entity = customerRepo.GetQueryale().FirstOrDefault(x => x.Id == command.CustomerId && x.PhoneNumber == command.PhoneNumber);



            
            if(Entity != null )
                return sr.To<string>(Entity?.Id);
            


            
            // Register Customer
            var customer = command.Adapt<CustomerEntity>();
            await customerRepo.InsertAsync(customer) ;
            _unitOfWork.SaveChanges();



            return sr.To<string>(Entity?.Id);




        }



        protected override ServiceResult<string> HandleOnError(Exception exp)
        {


            var sr = ServiceResult.Empty.SetError(exp.Message);
            sr.Error.ExteraFields = new KeyValueList<string, string>()
            {
                KeyValuePair.Create("InnerException",exp.InnerException?.Message),
                KeyValuePair.Create("Source",exp.Source),
                KeyValuePair.Create("StackTrace",exp.StackTrace)
            };
            return sr.To<string>();


        }


    }
}
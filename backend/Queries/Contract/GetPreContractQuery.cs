using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.ContractDtos;
using housingCooperative.Dtos.CustomerDtos;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Dtos.PlotDtos;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Queries.Contract
{

    public class GetPreContractQuery : QueryBase<ServiceResult<GetPreContractDetailOutputDto>>
    {
        public string PlotId { get; set; }
        public string CustomerId { get; set; }


        public GetPreContractQuery(string customerId, string id)
        {
            PlotId = id;
            CustomerId = customerId;

        }




        public class GetCustomersByAdminQueryHandler : BaseRequestQueryHandler<GetPreContractQuery, ServiceResult<GetPreContractDetailOutputDto>>
        {

            
            private readonly IUnitOfWork<housingCooperativeDBContext> _unitOfWork;


            public GetCustomersByAdminQueryHandler(ILogger<GetCustomersByAdminQueryHandler> logger, IUnitOfWork<housingCooperativeDBContext> unitOfWork) : base(logger)
            {
                _unitOfWork = unitOfWork;
            }


            protected async override Task<ServiceResult<GetPreContractDetailOutputDto>> HandleAsync(GetPreContractQuery query, CancellationToken cancellationToken)
            {




                var sr = ServiceResult.Empty;
                var contractRepo = _unitOfWork.GetRepository<PlotEntity>();
                var customerRepo = _unitOfWork.GetRepository<CustomerEntity>();

                var Customer = customerRepo.GetFirstOrDefault(predicate : x => x.Id == query.CustomerId);
                if(Customer==null)
                    return sr.SetError(Errors.CustomerNotFound.GetMessage() , Errors.CustomerNotFound.GetCode())
                    .SetMessage(Errors.CustomerNotFound.GetPersionMessage())
                    .To<GetPreContractDetailOutputDto>();

                var resultDto = contractRepo.GetQueryale()
                                        .Include(x => x.LandProject)
                                        .Where(x => x.Id == query.PlotId && !x.IsDeleted)
                                        .Select(x => new GetPreContractDetailOutputDto()
                                        {
                                            ProjectId = x.LandProject.Id,
                                            ProjectName = x.LandProject.Name,
                                            ProjectAddress = x.LandProject.Address,
                                            ProjectEngineerName = x.LandProject.EngineerName,
                                            ProjectDescription = x.LandProject.Description,
                                            PlotId = x.Id,
                                            PlotName = x.Name,
                                            PlotMeterage = x.Meterage,
                                            PlotDescription = x.Description,
                                            PlotPrePaymentAmount = x.PrePaymentAmount,
                                            PlotInstalmentAmount = x.InstalmentAmount,
                                            PlotInstalmentCount = x.InstalmentCount,
                                            CustomerId = Customer.Id,
                                            CustomerFirstName = Customer.FirstName,
                                            CustomerLastName = Customer.LastName,
                                        })
                                        .FirstOrDefault();




                return ServiceResult.Create<GetPreContractDetailOutputDto>(resultDto);


            }


            protected override ServiceResult<GetPreContractDetailOutputDto> HandleOnError(Exception exp)
            {


                var sr = ServiceResult.Empty.SetError(exp.Message);
                sr.Error.ExteraFields = new KeyValueList<string, string>()
                {
                    KeyValuePair.Create("InnerException",exp.InnerException?.Message),
                    KeyValuePair.Create("Source",exp.Source),
                    KeyValuePair.Create("StackTrace",exp.StackTrace)
                };
                return sr.To<GetPreContractDetailOutputDto>();


            }


        }
    }
}
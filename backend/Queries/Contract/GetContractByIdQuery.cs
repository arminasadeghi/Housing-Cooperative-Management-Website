using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.ContractDtos;
using housingCooperative.Dtos.CustomerDtos;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Dtos.PlotDtos;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Queries.Contract
{
    public class GetContractByIdQuery : QueryBase<ServiceResult<GetContractDetailOutputDto>>
    {
        public string ContractId { get; set; }


        public GetContractByIdQuery(string id)
        {
            ContractId = id;
        }




        public class GetCustomersByAdminQueryHandler : BaseRequestQueryHandler<GetContractByIdQuery, ServiceResult<GetContractDetailOutputDto>>
        {

            
            private readonly IUnitOfWork<housingCooperativeDBContext> _unitOfWork;


            public GetCustomersByAdminQueryHandler(ILogger<GetCustomersByAdminQueryHandler> logger, IUnitOfWork<housingCooperativeDBContext> unitOfWork) : base(logger)
            {
                _unitOfWork = unitOfWork;
            }


            protected async override Task<ServiceResult<GetContractDetailOutputDto>> HandleAsync(GetContractByIdQuery query, CancellationToken cancellationToken)
            {




                var sr = ServiceResult.Empty;
                var contractRepo = _unitOfWork.GetRepository<ContractEntity>();
                var resultDto = contractRepo.GetQueryale()
                                        .Include(x => x.Plot)   
                                        .Include(x => x.LandProject)
                                        .Include(x => x.Customer)
                                        .Include(x => x.Items.OrderBy(y => y.DueDate))
                                        .Where(x => x.Id == query.ContractId && !x.IsDeleted)
                                        .Select(x => new GetContractDetailOutputDto()
                                        {
                                            ContractId = x.Id,
                                            LandProject = new GetContractLandProjectOutputDto()
                                                {
                                                    Id = x.LandProject.Id,
                                                    Name = x.LandProject.Name,
                                                    Address = x.LandProject.Address,
                                                    EngineerName = x.LandProject.EngineerName,
                                                    Description = x.LandProject.Description
                                                },
                                            Plot =   new GetContractPlotOutputDto()
                                                {
                                                    Id = x.Plot.Id,
                                                    Name = x.Plot.Name,
                                                    Meterage = x.Plot.Meterage,
                                                    Description = x.Plot.Description
                                                },
                                            Customer = new GetContractCustomerOutputDto()
                                                {
                                                    Id = x.Customer.Id,
                                                    FirstName = x.Customer.FirstName,
                                                    LastName = x.Customer.LastName
                                                },
                                             PrePaymentAmount = x.PrePaymentAmount,
                                             InstalmentAmount = x.InstalmentAmount,
                                             InstalmentCount = x.Plot.InstalmentCount,
                                             StartDate = x.StartDate,
                                             EndDate = x.EndDate,
                                            Items = x.Items.Select(y => new GetAllContrcatItemsOutputDto()
                                            {
                                                Id = y.Id,
                                                InstalmentAmount = y.InstalmentAmount,
                                                PaidAmount = y.PaidAmount,
                                                DueDate = y.DueDate,
                                            }).OrderBy(x => x.DueDate)
                                            .ToList()
                                        })
                                        .FirstOrDefault();


                return ServiceResult.Create<GetContractDetailOutputDto>(resultDto);


            }


            protected override ServiceResult<GetContractDetailOutputDto> HandleOnError(Exception exp)
            {


                var sr = ServiceResult.Empty.SetError(exp.Message);
                sr.Error.ExteraFields = new KeyValueList<string, string>()
                {
                    KeyValuePair.Create("InnerException",exp.InnerException?.Message),
                    KeyValuePair.Create("Source",exp.Source),
                    KeyValuePair.Create("StackTrace",exp.StackTrace)
                };
                return sr.To<GetContractDetailOutputDto>();


            }


        }
    }
}
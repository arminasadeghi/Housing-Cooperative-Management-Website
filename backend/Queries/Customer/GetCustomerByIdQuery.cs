using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.CustomerDtos;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Queries.Customer
{
    public class GetCustomerByIdQuery : QueryBase<ServiceResult<GetCustomerInfoOutputDto>>
    {
        public string CustomerId { get; set; }


        public GetCustomerByIdQuery(string id)
        {
            CustomerId = id;
        }




        public class GetCustomersByAdminQueryHandler : BaseRequestQueryHandler<GetCustomerByIdQuery, ServiceResult<GetCustomerInfoOutputDto>>
        {

            
            private readonly IUnitOfWork<housingCooperativeDBContext> _unitOfWork;


            public GetCustomersByAdminQueryHandler(ILogger<GetCustomersByAdminQueryHandler> logger, IUnitOfWork<housingCooperativeDBContext> unitOfWork) : base(logger)
            {
                _unitOfWork = unitOfWork;
            }


            protected async override Task<ServiceResult<GetCustomerInfoOutputDto>> HandleAsync(GetCustomerByIdQuery query, CancellationToken cancellationToken)
            {




                var sr = ServiceResult.Empty;
                var customerRepo = _unitOfWork.GetRepository<CustomerEntity>();
                var entity = customerRepo.GetQueryale().FirstOrDefault(x => x.Id == query.CustomerId);


                if(entity == null )
                    return sr.SetError(Errors.CustomerNotFound.GetMessage() , Errors.CustomerNotFound.GetCode())
                      .SetMessage(Errors.CustomerNotFound.GetPersionMessage())
                      .To<GetCustomerInfoOutputDto>();


                var resultDto = entity.Adapt<GetCustomerInfoOutputDto>();



                return ServiceResult.Create<GetCustomerInfoOutputDto>(resultDto);


            }


            protected override ServiceResult<GetCustomerInfoOutputDto> HandleOnError(Exception exp)
            {


                var sr = ServiceResult.Empty.SetError(exp.Message);
                sr.Error.ExteraFields = new KeyValueList<string, string>()
                {
                    KeyValuePair.Create("InnerException",exp.InnerException?.Message),
                    KeyValuePair.Create("Source",exp.Source),
                    KeyValuePair.Create("StackTrace",exp.StackTrace)
                };
                return sr.To<GetCustomerInfoOutputDto>();


            }


        }
    }
}
using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.ContractDtos;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.Core.Expressions;
using Library.DDD.DataAccessLayer.PagedList;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Queries.Contract
{
    public class GetCustomerContractsQuery: QueryBase<ServiceResult<IPagedList<GetAllContractsOutputDto>>>
    {
        public string CustomerId { get; set; }
        public SearchFilter sf { get; set; }

        public GetCustomerContractsQuery(string customerId, SearchFilter sf)
        {
            CustomerId = customerId;
            this.sf = sf;
        }

        public GetCustomerContractsQuery()
        {

        }


        public class GetCustomerContractsQueryHandler : BaseRequestQueryHandler<GetCustomerContractsQuery, ServiceResult<IPagedList<GetAllContractsOutputDto>>>
        {
            private readonly IUnitOfWork<housingCooperativeDBContext> unitOfWork;
            public GetCustomerContractsQueryHandler(ILogger<GetCustomerContractsQueryHandler> logger, IUnitOfWork<housingCooperativeDBContext> _unitOfWork) : base(logger)
            {
                unitOfWork = _unitOfWork;
            }

            protected override async Task<ServiceResult<IPagedList<GetAllContractsOutputDto>>> HandleAsync(GetCustomerContractsQuery query, CancellationToken cancellationToken)
            {

                var sr = ServiceResult.Empty;
                var contractRepo = unitOfWork.GetRepository<ContractEntity>();


                    var AllCustomerProjectsResult = contractRepo.GetPagedList
                                (   x => new GetAllContractsOutputDto(x.Id,
                                                                        x.LandProject.Id,
                                                                        x.LandProject.Name ,
                                                                        x.Plot.Id,
                                                                        x.Plot.Name ,
                                                                        x.Plot.Meterage),
                                    x => x.CustomerId == query.CustomerId && !x.IsDeleted,
                                    x => x.OrderByDescending(y => y.CreatedAt),
                                    x => x.Include(y => y.LandProject).Include(y => y.Plot),
                                    query.sf.PgNumber ,
                                    query.sf.PgSize , 
                                    true);



                return ServiceResult.Create<IPagedList<GetAllContractsOutputDto>>(AllCustomerProjectsResult);
            }

            protected override ServiceResult<IPagedList<GetAllContractsOutputDto>> HandleOnError(System.Exception exp)
            {
                var sr = ServiceResult.Empty;
                sr = sr.SetError("UnHandled");
                sr.Error.ExteraFields = new KeyValueList<string, string>(){
                    new KeyValuePair<string, string>(nameof(exp.Message),exp.Message),
                    new KeyValuePair<string, string>(nameof(exp.Source),exp.Source),
                    new KeyValuePair<string, string>(nameof(exp.InnerException),exp.InnerException?.Message ?? ""),
                };

                return sr.To<IPagedList<GetAllContractsOutputDto>>();
            }
        }
        
    }
}
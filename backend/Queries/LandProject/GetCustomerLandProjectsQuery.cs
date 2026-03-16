using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.Core.Expressions;
using Library.DDD.DataAccessLayer.PagedList;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Queries.LandProject
{
    public class GetCustomerLandProjectsQuery: QueryBase<ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>>
    {
        public string CustomerId { get; set; }
        public SearchFilter sf { get; set; }

        public GetCustomerLandProjectsQuery(string customerId, SearchFilter sf)
        {
            CustomerId = customerId;
            this.sf = sf;
        }

        public GetCustomerLandProjectsQuery()
        {

        }


        public class GetCustomerLandProjectsQueryHandler : BaseRequestQueryHandler<GetCustomerLandProjectsQuery, ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>>
        {
            private readonly IUnitOfWork<housingCooperativeDBContext> unitOfWork;
            public GetCustomerLandProjectsQueryHandler(ILogger<GetCustomerLandProjectsQueryHandler> logger, IUnitOfWork<housingCooperativeDBContext> _unitOfWork) : base(logger)
            {
                unitOfWork = _unitOfWork;
            }

            protected override async Task<ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>> HandleAsync(GetCustomerLandProjectsQuery query, CancellationToken cancellationToken)
            {

                var sr = ServiceResult.Empty;
                var contractRepo = unitOfWork.GetRepository<ContractEntity>();


                    var AllCustomerProjectsResult = contractRepo.GetPagedList
                                (   x => new GetAllLandProjectsOutputDto(x.LandProject.Id,
                                                                        x.LandProject.Name,
                                                                        x.LandProject.Address ,
                                                                        x.LandProject.ImageList.FirstOrDefault(),
                                                                        x.LandProject.EngineerName ,
                                                                        x.LandProject.Description ,
                                                                        x.LandProject.StartDate ,
                                                                        x.LandProject.EndDate ,
                                                                        x.LandProject.EstimatedStartDate,
                                                                        x.LandProject.EstimatedEndDate),
                                    x => x.CustomerId == query.CustomerId && !x.IsDeleted,
                                    x => x.OrderByDescending(y => y.CreatedAt),
                                    x => x.Include(y => y.LandProject),
                                    query.sf.PgNumber ,
                                    query.sf.PgSize , 
                                    true);



                return ServiceResult.Create<IPagedList<GetAllLandProjectsOutputDto>>(AllCustomerProjectsResult);
            }

            protected override ServiceResult<IPagedList<GetAllLandProjectsOutputDto>> HandleOnError(System.Exception exp)
            {
                var sr = ServiceResult.Empty;
                sr = sr.SetError("UnHandled");
                sr.Error.ExteraFields = new KeyValueList<string, string>(){
                    new KeyValuePair<string, string>(nameof(exp.Message),exp.Message),
                    new KeyValuePair<string, string>(nameof(exp.Source),exp.Source),
                    new KeyValuePair<string, string>(nameof(exp.InnerException),exp.InnerException?.Message ?? ""),
                };

                return sr.To<IPagedList<GetAllLandProjectsOutputDto>>();
            }
        }
        
    }
}
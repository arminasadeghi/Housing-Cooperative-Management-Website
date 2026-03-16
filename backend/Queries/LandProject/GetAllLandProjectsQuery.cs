using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.Core.Expressions;
using Library.DDD.DataAccessLayer.PagedList;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Queries.LandProject
{
    public class GetAllLandProjectsQuery: QueryBase<ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>>
    {
        public SearchFilter sf { get; set; }

        public GetAllLandProjectsQuery(SearchFilter sf)
        {
            this.sf = sf;
        }

        public GetAllLandProjectsQuery()
        {

        }


        public class GetAllLandProjectsQueryHandler : BaseRequestQueryHandler<GetAllLandProjectsQuery, ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>>
        {
            private readonly IUnitOfWork<housingCooperativeDBContext> unitOfWork;
            public GetAllLandProjectsQueryHandler(ILogger<GetAllLandProjectsQueryHandler> logger, IUnitOfWork<housingCooperativeDBContext> _unitOfWork) : base(logger)
            {
                unitOfWork = _unitOfWork;
            }

            protected override async Task<ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>> HandleAsync(GetAllLandProjectsQuery query, CancellationToken cancellationToken)
            {

                var sr = ServiceResult.Empty;
                var landProjectRepo = unitOfWork.GetRepository<LandProjectEntity>();


                    var AllCustomerProjectsResult = landProjectRepo.GetPagedList
                                (   x => new GetAllLandProjectsOutputDto(x.Id,
                                                                        x.Name,
                                                                        x.Address ,
                                                                        x.ImageList.FirstOrDefault(),
                                                                        x.EngineerName ,
                                                                        x.Description ,
                                                                        x.StartDate ,
                                                                        x.EndDate ,
                                                                        x.EstimatedStartDate,
                                                                        x.EstimatedEndDate),
                                    null,
                                    x => x.OrderByDescending(y => y.CreatedAt),
                                    null,
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
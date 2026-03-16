using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Dtos.PhaseDtos;
using housingCooperative.Dtos.PlotDtos;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Queries.LandProject
{
    public class GetLandProjectByIdQuery : QueryBase<ServiceResult<GetLandProjectDetailOutputDto>>
    {
        public string ProjectId { get; set; }


        public GetLandProjectByIdQuery(string id)
        {
            ProjectId = id;
        }




        public class GetCustomersByAdminQueryHandler : BaseRequestQueryHandler<GetLandProjectByIdQuery, ServiceResult<GetLandProjectDetailOutputDto>>
        {

            
            private readonly IUnitOfWork<housingCooperativeDBContext> _unitOfWork;


            public GetCustomersByAdminQueryHandler(ILogger<GetCustomersByAdminQueryHandler> logger, IUnitOfWork<housingCooperativeDBContext> unitOfWork) : base(logger)
            {
                _unitOfWork = unitOfWork;
            }


            protected async override Task<ServiceResult<GetLandProjectDetailOutputDto>> HandleAsync(GetLandProjectByIdQuery query, CancellationToken cancellationToken)
            {




                var sr = ServiceResult.Empty;
                var projectRepo = _unitOfWork.GetRepository<LandProjectEntity>();
                var entity = projectRepo.GetQueryale()
                                        .Include(x => x.Plots.Where(y => String.IsNullOrEmpty(y.ContractId)))   
                                        .Include(x => x.Phases)
                                        .FirstOrDefault(x => x.Id == query.ProjectId && !x.IsDeleted);


                if(entity == null )
                    return sr.SetError(Errors.ProjectNotFound.GetMessage() , Errors.ProjectNotFound.GetCode())
                      .SetMessage(Errors.ProjectNotFound.GetPersionMessage())
                      .To<GetLandProjectDetailOutputDto>();


                var resultDto = entity.Adapt<GetLandProjectDetailOutputDto>();
                resultDto.Phases = entity.Phases
                                    .OrderBy(x => x.Order)
                                    .Adapt<List<GetProjectPhasesOutputDto>>();
                                    
                resultDto.Plots = entity.Plots
                                    .GroupBy(x => x.Meterage)
                                    .Select(x => x.OrderBy(e => e.Value).FirstOrDefault())
                                    .ToList().Adapt<List<GetProjectPlotsOutputDto>>();

                return ServiceResult.Create<GetLandProjectDetailOutputDto>(resultDto);


            }


            protected override ServiceResult<GetLandProjectDetailOutputDto> HandleOnError(Exception exp)
            {


                var sr = ServiceResult.Empty.SetError(exp.Message);
                sr.Error.ExteraFields = new KeyValueList<string, string>()
                {
                    KeyValuePair.Create("InnerException",exp.InnerException?.Message),
                    KeyValuePair.Create("Source",exp.Source),
                    KeyValuePair.Create("StackTrace",exp.StackTrace)
                };
                return sr.To<GetLandProjectDetailOutputDto>();


            }


        }
    }
}
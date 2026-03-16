using housingCooperative.Commands.CustomerCommands;
using housingCooperative.Commands.LandProjects;
using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.ContractDtos;
using housingCooperative.Dtos.CustomerDtos;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Dtos.PaymentDtos;
using housingCooperative.Infrastructures.DbContexts;
using housingCooperative.Queries.Contract;
using housingCooperative.Queries.Customer;
using housingCooperative.Queries.LandProject;
using housingCooperative.Queries.Payment;
using Library.DDD.Core.Expressions;
using Library.DDD.DataAccessLayer.PagedList;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ContributionController : LibraryBaseController
    {

                private readonly IMediator _mediator;
        private readonly IUserHelper _userHelper;
        private readonly IUnitOfWork<housingCooperativeDBContext> unitOfWork;

        public ContributionController(ILogger<ContributionController> logger,
        IUserHelper userHelper, IMediator mediator, IUnitOfWork<housingCooperativeDBContext> unitOfWork) : base(logger, userHelper)
        {
            _mediator = mediator;
            _userHelper = userHelper;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  Update CustomerInfo by User
        /// </summary>
        /// <remarks>
        ///  CustomerId and PhoneNumber cant change. FirstName, LastName and NationalId are required
        /// </remarks>
        /// <response code="400"> if national code is invalid  </response>  
        /// <response code="404"> if customer is not found  </response>  
        /// <response code="405"> if type of request is wrong or input data are wrong  </response> 
        /// <returns> Id Of The Updated Customer </returns>
        [ProducesResponseType(typeof(ServiceResult<string>), 200)]
        [HttpPut("Customer/Update")]
        [Authorize]
        public async Task<ServiceResult<string>> UpdateCustomerByUser(UpdateCustomerInputDto updateCustomerInputDto)
        {

            var updateCustomerByUserCommand = updateCustomerInputDto.Adapt<UpdateCustomerByUserCommand>();
            updateCustomerByUserCommand.CustomerId = IUser.Id;
            return await _mediator.Send<ServiceResult<string>>(updateCustomerByUserCommand);

        }

        /// <summary>
        ///  Get Logined In CustomerInfo
        /// </summary>
        /// <response code="404"> if customer is not found  </response> 
        /// <returns> Get Logined In CustomerInfo </returns>
        [HttpGet("Customer/Info")]
        [ProducesResponseType(typeof(ServiceResult<GetCustomerInfoOutputDto>), 200)]
        [Authorize]
        public async Task<ActionResult> GetCustomerInformationByUser()
        {
            var result = await _mediator.Send<ServiceResult<GetCustomerInfoOutputDto>>(new GetCustomerByIdQuery(IUser.Id));
            return await result?.AsyncResult();
        }

        /// <summary>
        ///  Get All Projects By SearchFilter
        /// </summary>
        /// <returns> List Of Projects  </returns>
        [HttpPost("Project/All")]
        [ProducesResponseType(typeof(ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>), 200)]
        // [Authorize]
        public async Task<ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>> GetAllProjects(SearchFilter sf)
        {
            
                return await _mediator.Send<ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>>(new GetAllLandProjectsQuery(sf));

        }

        /// <summary>
        ///  Get All Projects of a Customer By SearchFilter
        /// </summary> 
        /// <returns> List Of Projects of a customer </returns>
        [HttpPost("Project/All/Customer")]
        [ProducesResponseType(typeof(ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>), 200)]
        [Authorize]
        public async Task<ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>> GetAllProjectsOfCustomer(SearchFilter sf)
        {
            return await _mediator.Send<ServiceResult<IPagedList<GetAllLandProjectsOutputDto>>>(new GetCustomerLandProjectsQuery(IUser.Id,sf));
        }





        /// <summary>
        ///  Get Project Detail By Id
        /// </summary>
        /// <returns> Project Detail  </returns>
        /// <response code="404"> if Project is not found  </response> 
        [HttpGet("Project/Detail/{ProjectId}")]
        [ProducesResponseType(typeof(ServiceResult<GetLandProjectDetailOutputDto>), 200)]
        // [Authorize]
        public async Task<ServiceResult<GetLandProjectDetailOutputDto>> GetProjectDetail([FromRoute] string ProjectId)
        {
            
            return await _mediator.Send<ServiceResult<GetLandProjectDetailOutputDto>>(new GetLandProjectByIdQuery(ProjectId));

        }

        [HttpPost("Contract/create")]
        [Authorize]
        public async Task<ServiceResult<string>> CreateContract([ FromBody ]CreateContractByUserDto createContractByUserDto)
        {
            return await _mediator.Send<ServiceResult<string>>(new CreateContractByUserCommand(IUser.Id , createContractByUserDto));
        }
        /// <summary>
        ///  Get All Contracts of a Customer By SearchFilter
        /// </summary> 
        /// <returns> List Of Contracts of a customer </returns>
        [HttpPost("Contract/All/Customer")]
        [ProducesResponseType(typeof(ServiceResult<IPagedList<GetAllContractsOutputDto>>), 200)]
        [Authorize]
        public async Task<ServiceResult<IPagedList<GetAllContractsOutputDto>>> GetAllContractsOfCustomer(SearchFilter sf)
        {
            return await _mediator.Send<ServiceResult<IPagedList<GetAllContractsOutputDto>>>(new GetCustomerContractsQuery(IUser.Id,sf));
        }

        /// <summary>
        ///  Get Contract Detail By Id
        /// </summary>
        /// <returns> Contract Detail  </returns>
        /// <response code="404"> if Contract is not found  </response> 
        [HttpGet("Contract/Detail/{ContractId}")]
        [ProducesResponseType(typeof(ServiceResult<GetContractDetailOutputDto>), 200)]
        [Authorize]
        public async Task<ServiceResult<GetContractDetailOutputDto>> GetContractDetail([FromRoute] string ContractId)
        {
            
            return await _mediator.Send<ServiceResult<GetContractDetailOutputDto>>(new GetContractByIdQuery(ContractId));

        }

        [HttpPut("Contract/Pay/{ContractId}/{PayAmount}")]
        [Authorize]
        public async Task<ServiceResult<string>> PayContract([FromRoute] string ContractId , [FromRoute] long PayAmount)
        {
            var userId =  IUser.Id;
            return await _mediator.Send<ServiceResult<string>>(new PayContractByUserCommand( userId ,ContractId , PayAmount));
        }

        /// <summary>
        ///  Get Contract Detail By Id
        /// </summary>
        /// <returns> Contract Detail  </returns>
        /// <response code="404"> if Contract is not found  </response> 
        // [HttpGet("Contract/Detail/{PlotId}")]
        // [ProducesResponseType(typeof(ServiceResult<GetPreContractDetailOutputDto>), 200)]
        // [Authorize]
        // public async Task<ServiceResult<GetPreContractDetailOutputDto>> GetPreContractDetail([FromRoute] string PlotId)
        // {
            
        //     return await _mediator.Send<ServiceResult<GetPreContractDetailOutputDto>>(new GetPreContractQuery(IUser.Id,PlotId));

        // }


        /// <summary>
        ///  Get user Payments
        /// </summary>
        /// <returns> Payment Detail  </returns> 
        [HttpPost("Payment/All/Customer")]
        [ProducesResponseType(typeof(List<GetCustomerPaymentsDto>), 200)]
        [Authorize]
        public async Task<ServiceResult<List<GetCustomerPaymentsDto>>> GetCustomerPayments()
        {
            
             var userId =  IUser.Id;
            return await _mediator.Send<ServiceResult<List<GetCustomerPaymentsDto>>>(new GetCustomerPaymentQuery(userId));

        }


        /// <summary>
        ///  Get user Payments
        /// </summary>
        /// <returns> Payment Detail  </returns> 
        [HttpPost("Payment/Detail/Customer/{paymentId}")]
        [ProducesResponseType(typeof(List<GetPaymentDetailDto>), 200)]
        [Authorize]
        public async Task<ServiceResult<List<GetPaymentDetailDto>>> GetCustomerPayment([FromRoute] string paymentId)
        {
            
            return await _mediator.Send<ServiceResult<List<GetPaymentDetailDto>>>(new GetCustomerPaymentDetailQuery(paymentId));

        }


        
        /// <summary>
        ///  Get All Customers
        /// </summary>
        /// <returns> Get All Customers  </returns> 
        [HttpPost("admin/customer/all")]
        [ProducesResponseType(typeof(List<GetCustomerInfoOutputDto>), 200)]
        public async Task<ServiceResult<IPagedList<GetCustomerInfoOutputDto>>> GetAllCustomers([FromBody] SearchFilter searchFilter)
        {
                
                var sr = ServiceResult.Empty;
                var customerRepo = unitOfWork.GetRepository<CustomerEntity>();


                    var AllCustomer = customerRepo.GetPagedList
                (   e => new GetCustomerInfoOutputDto()
                {
                    Address = e.Address ,
                    BirthDate = e.BirthDate , 
                    City = e.City ,
                    FirstName = e.FirstName ,
                    LastName = e.LastName ,
                    Gender = e.Gender ,
                    Id = e.Id ,
                    JobTitle = e.JobTitle ,
                    NationalId = e.NationalId ,
                    PhoneNumber = e.PhoneNumber ,
                },
                    null,
                    x => x.OrderByDescending(y => y.CreatedAt),
                    null,
                    searchFilter.PgNumber ,
                    searchFilter.PgSize , 
                    true);


                return ServiceResult.Create<IPagedList<GetCustomerInfoOutputDto>>(AllCustomer);

        }



        /// <summary>
        ///  Get All Payments
        /// </summary>
        /// <returns> Get All Payments  </returns> 
        [HttpPost("admin/payments/all")]
        [ProducesResponseType(typeof(List<GetCustomerInfoOutputDto>), 200)]
        public async Task<ServiceResult<List<GetPaymentDetailByAdminDto>>> GetAllPayment()
        {
                
        var paidItemRepo = unitOfWork.GetRepository<ContractPaidItemEntity>();


                var payments = paidItemRepo.GetQueryale()
                                            .Include(x => x.ContractItem)
                                            .ThenInclude(x => x.Contract)
                                                .ThenInclude(x => x.Customer)
                                            .Include(x => x.ContractItem)
                                            .ThenInclude(x => x.Contract)
                                                .ThenInclude(x => x.Plot)
                                            .ThenInclude(x => x.LandProject)
                                            .GroupBy(x => x.PaymentId)
                                            .Select(x => new GetPaymentDetailByAdminDto()
                                            {
                                                Amount = x.Sum(x => x.Amount) ,
                                                Status = Dtos.Enums.PaymentStatusEnum.Success ,
                                                Id = x.Key ,
                                                Type = Dtos.Enums.PaymentTypeEnum.Deposit ,
                                                CreatedAt = x.FirstOrDefault().CreatedAt ,
                                                PlotName = x.FirstOrDefault().ContractItem.Contract.Plot.Name ,
                                                ProjectName = x.FirstOrDefault().ContractItem.Contract.LandProject.Name ,
                                                CustomerInfo = new GetCustomerInfoOutputDto()
                                                {
                                                    
                                                    Id = x.FirstOrDefault().ContractItem.Contract.Customer.FirstName ,
                                                    FirstName = x.FirstOrDefault().ContractItem.Contract.Customer.FirstName ,
                                                    LastName = x.FirstOrDefault().ContractItem.Contract.Customer.LastName ,
                                                    PhoneNumber = x.FirstOrDefault().ContractItem.Contract.Customer.PhoneNumber ,
                                                    Address =    x.FirstOrDefault().ContractItem.Contract.Customer.Address ,
                                                    BirthDate =  x.FirstOrDefault().ContractItem.Contract.Customer.BirthDate , 
                                                    City =       x.FirstOrDefault().ContractItem.Contract.Customer.City ,
                                                    Gender =     x.FirstOrDefault().ContractItem.Contract.Customer.Gender ,
                                                    JobTitle =   x.FirstOrDefault().ContractItem.Contract.Customer.JobTitle ,
                                                    NationalId = x.FirstOrDefault().ContractItem.Contract.Customer.NationalId ,
                                                }
                                            })
                                            .ToList();

                return ServiceResult.Create<List<GetPaymentDetailByAdminDto>>(payments);

        }

        /// <summary>
        ///  create project
        /// </summary>
        /// <returns> create project  </returns> 
        [HttpPost("admin/project/create")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<ServiceResult<string>> CreateProject([FromBody] CreateProjectInputDto dto )
        {

            var projectRepo = unitOfWork.GetRepository<LandProjectEntity>();
            var newProj = new LandProjectEntity(dto);

            projectRepo.Insert(newProj);
            unitOfWork.SaveChanges();

            return ServiceResult.Create<string>(String.Empty);
 
        }


    }
}
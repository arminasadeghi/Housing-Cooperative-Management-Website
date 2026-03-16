using housingCooperative.Commands.CustomerCommands;
using housingCooperative.Dtos.CustomerDtos;

namespace housingCooperative.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InternalContributionController : LibraryBaseController
    {
        private readonly IMediator _mediator;


        public InternalContributionController(ILogger<InternalContributionController> logger,
        IUserHelper userHelper, IMediator mediator ) : base(logger, userHelper)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///  Register a new user
        /// </summary>
        /// <returns> Return empty string </returns>
        [HttpPost("Customer/Register")]
        [ProducesResponseType(typeof(ServiceResult<string>), 200)]
        public async Task<ServiceResult<string>> RegisterCustomer(RegisterCustomerInputDto registerCustomerInputDto)
        {
            return await _mediator.Send<ServiceResult<string>>(registerCustomerInputDto.Adapt<CreateCustomerByIdentityCommand>());
        }

        
    }
}
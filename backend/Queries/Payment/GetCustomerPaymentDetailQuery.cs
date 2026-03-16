using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.PaymentDtos;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.Core.BaseModels;
using Library.DDD.Core.Expressions;
using Library.DDD.DataAccessLayer.PagedList;
using Library.DDD.DataAccessLayer.UnitOfWork;

namespace housingCooperative.Queries.Payment
{
    public class GetCustomerPaymentDetailQuery : QueryBase<ServiceResult<List<GetPaymentDetailDto>>>
    {
        public GetCustomerPaymentDetailQuery(string paymentId )
        {
            this.paymentId = paymentId;
        }

        public string paymentId { get; }
        }

    public class GetCustomerPaymentDetailQueryHandler : BaseRequestQueryHandler<GetCustomerPaymentDetailQuery, ServiceResult<List<GetPaymentDetailDto>>>
    {
        
        private readonly IUnitOfWork<housingCooperativeDBContext> unitOfWork;

        public GetCustomerPaymentDetailQueryHandler(Microsoft.Extensions.Logging.ILogger<GetCustomerPaymentDetailQueryHandler> logger, IUnitOfWork<housingCooperativeDBContext> unitOfWork) : base(logger)
        {
            this.unitOfWork = unitOfWork;
        }

        protected async override Task<ServiceResult<List<GetPaymentDetailDto>>> HandleAsync(GetCustomerPaymentDetailQuery query, CancellationToken cancellationToken)
        {
            
                var sr = ServiceResult.Empty;
                var paidItemRepo = unitOfWork.GetRepository<ContractPaidItemEntity>();


                var customerPayments = paidItemRepo.GetQueryale()
                                            .Include(x => x.ContractItem)
                                            .ThenInclude(x => x.Contract)
                                                .ThenInclude(x => x.Plot)
                                            .ThenInclude(x => x.LandProject)
                                            .Where(x => x.PaymentId == query.paymentId)
                                            .Select(x => new GetPaymentDetailDto()
                                            {
                                                Amount = x.Amount ,
                                                Status = Dtos.Enums.PaymentStatusEnum.Success ,
                                                Id = x.Id ,
                                                Type = Dtos.Enums.PaymentTypeEnum.Deposit ,
                                                CreatedAt = x.CreatedAt , 
                                                ProjectName = x.ContractItem.Contract.LandProject.Name ?? String.Empty ,
                                                PlotName = x.ContractItem.Contract.Plot.Name
                                            })
                                            .ToList();



                return ServiceResult.Create<List<GetPaymentDetailDto>>(customerPayments);
        }

    }
}
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
    public class GetCustomerPaymentQuery : QueryBase<ServiceResult<List<GetCustomerPaymentsDto>>>
    {
        public GetCustomerPaymentQuery(string customerId )
        {
            CustomerId = customerId;
        }

        public string CustomerId { get; }
        }

    public class GetCustomerPaymentQueryHandler : BaseRequestQueryHandler<GetCustomerPaymentQuery, ServiceResult<List<GetCustomerPaymentsDto>>>
    {
        
        private readonly IUnitOfWork<housingCooperativeDBContext> unitOfWork;

        public GetCustomerPaymentQueryHandler(Microsoft.Extensions.Logging.ILogger<GetCustomerPaymentQueryHandler> logger, IUnitOfWork<housingCooperativeDBContext> unitOfWork) : base(logger)
        {
            this.unitOfWork = unitOfWork;
        }

        protected async override Task<ServiceResult<List<GetCustomerPaymentsDto>>> HandleAsync(GetCustomerPaymentQuery query, CancellationToken cancellationToken)
        {
            
                var sr = ServiceResult.Empty;
                var paidItemRepo = unitOfWork.GetRepository<ContractPaidItemEntity>();


                var customerPayments = paidItemRepo.GetQueryale()
                                            .Include(x => x.ContractItem)
                                            .ThenInclude(x => x.Contract)
                                            .Where(x => x.ContractItem.Contract.CustomerId == query.CustomerId)
                                            .GroupBy(x => x.PaymentId)
                                            .Select(x => new GetCustomerPaymentsDto()
                                            {
                                                Amount = x.Sum(x => x.Amount) ,
                                                Status = Dtos.Enums.PaymentStatusEnum.Success ,
                                                Id = x.Key ,
                                                Type = Dtos.Enums.PaymentTypeEnum.Deposit ,
                                                CreatedAt = x.FirstOrDefault().CreatedAt
                                            })
                                            .ToList();



                return ServiceResult.Create<List<GetCustomerPaymentsDto>>(customerPayments);
        }

    }
}
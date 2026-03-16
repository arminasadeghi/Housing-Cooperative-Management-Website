using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.Core.BaseModels;
using Library.DDD.Core.Expressions;
using Library.DDD.DataAccessLayer.UnitOfWork;
using Z.EntityFramework.Plus;

namespace housingCooperative.Commands.LandProjects
{
    public class PayContractByUserCommand : CommandBase<ServiceResult<string>>
    {



        public PayContractByUserCommand(string userId, string contractId, long payAmount)
        {
            UserId = userId;
            ContractId = contractId;
            PayAmount = payAmount;
        }

        public string UserId { get; }
        public string ContractId { get; }
        public long PayAmount { get; }
    }


    public class PayContractByUserCommandHandler : BaseRequestCommandHandler<PayContractByUserCommand, ServiceResult<string>>
    {

        private readonly IUnitOfWork<housingCooperativeDBContext> _unitOfWork;

        public PayContractByUserCommandHandler(Microsoft.Extensions.Logging.ILogger<PayContractByUserCommand> logger, IUnitOfWork<housingCooperativeDBContext> unitOfWork) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        protected async override Task<ServiceResult<string>> HandleAsync(PayContractByUserCommand command, CancellationToken cancellationToken)
        {

            var sr = ServiceResult.Empty ; 
            var contractRepo = _unitOfWork.GetRepository<ContractEntity>();
            var plotRepo = _unitOfWork.GetRepository<PlotEntity>();
            var contractPaiedRepo = _unitOfWork.GetRepository<ContractPaidItemEntity>();


            // check plot should be free
            var contract = contractRepo.GetQueryale()
                    .Include(x => x.Items)
                    .Include(x => x.Customer)
                    .Include(x => x.LandProject)
                    .FirstOrDefault(x => x.Id == command.ContractId && x.CustomerId == command.UserId);
            

            if(contract == null )
                    return sr.SetError(Errors.PlotIsNotFree.GetMessage() , Errors.PlotIsNotFree.GetCode())
                    .SetMessage(Errors.PlotIsNotFree.GetPersionMessage())
                    .To<string>();


            if(contract.Items.All(x => x.PaidAmount == x.InstalmentAmount) || contract.Items.Sum(x => x.RemainAmount) < command.PayAmount)
                    return sr.SetError(Errors.NoItemExistForPay.GetMessage() , Errors.NoItemExistForPay.GetCode())
                    .SetMessage(Errors.NoItemExistForPay.GetPersionMessage())
                    .To<string>();


            var paidItems = contract.Pay(command.PayAmount);
            var paymentId = Guid.NewGuid().ToString();
            paidItems.ForEach(x =>
            {
                contractPaiedRepo.Insert(new ContractPaidItemEntity(x , command.PayAmount , paymentId ));
            });

            _unitOfWork.SaveChanges();

            return ServiceResult.Create<string>(contract.Id);
        }
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Infrastructures.DbContexts;
using Library.DDD.Core.Expressions;
using Library.DDD.DataAccessLayer.UnitOfWork;
using Z.EntityFramework.Plus;

namespace housingCooperative.Commands.LandProjects
{
    public class CreateContractByUserCommand : CommandBase<ServiceResult<string>>
    {
        public string userId;
        public CreateContractByUserDto dto;


        public CreateContractByUserCommand(string userId, CreateContractByUserDto dto)
        {
            this.userId = userId;
            this.dto = dto;
        }
    }


    public class CreateContractByUserCommandHandler : BaseRequestCommandHandler<CreateContractByUserCommand, ServiceResult<string>>
    {

        private readonly IUnitOfWork<housingCooperativeDBContext> _unitOfWork;

        public CreateContractByUserCommandHandler(Microsoft.Extensions.Logging.ILogger<CreateContractByUserCommand> logger, IUnitOfWork<housingCooperativeDBContext> unitOfWork) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        protected async override Task<ServiceResult<string>> HandleAsync(CreateContractByUserCommand command, CancellationToken cancellationToken)
        {

                        var sr = ServiceResult.Empty ; 
            var contractRepo = _unitOfWork.GetRepository<ContractEntity>();
            var plotRepo = _unitOfWork.GetRepository<PlotEntity>();


            // check plot should be free
            var isExistContractOnPlot = contractRepo.GetQueryale().Any(x => x.PlotId == command.dto.PlotId);

            if(isExistContractOnPlot)
                    return sr.SetError(Errors.PlotIsNotFree.GetMessage() , Errors.PlotIsNotFree.GetCode())
                    .SetMessage(Errors.PlotIsNotFree.GetPersionMessage())
                    .To<string>();

            
            var plot = plotRepo.GetQueryale().FirstOrDefault(x => x.Id == command.dto.PlotId);

            if(plot is null )
                    return sr.SetError(Errors.PlotNotFound.GetMessage() , Errors.PlotNotFound.GetCode())
                    .SetMessage(Errors.PlotNotFound.GetPersionMessage())
                    .To<string>();

            // create contract with items 
            var Contract = new ContractEntity(command.userId , plot.Id , plot.ProjectId , plot.Value , plot.PrePaymentAmount , plot.PrePaymentAmount , plot.InstalmentAmount , "Descripton" );
            contractRepo.Insert(Contract);
            plot.SetContract(Contract.Id);
            plotRepo.Update(plot);
            _unitOfWork.SaveChanges();

            return ServiceResult.Create<string>(Contract.Id);
        }
    }


}
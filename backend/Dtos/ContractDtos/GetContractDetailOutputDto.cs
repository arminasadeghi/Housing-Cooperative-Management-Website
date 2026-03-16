using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.CustomerDtos;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Dtos.PlotDtos;

namespace housingCooperative.Dtos.ContractDtos
{
    public class GetContractDetailOutputDto
    {
        public string ContractId { get; set; }   
        public GetContractLandProjectOutputDto? LandProject {get;set;}
        public GetContractPlotOutputDto? Plot {get;set;}
        public GetContractCustomerOutputDto? Customer {get; set;}
        public List<GetAllContrcatItemsOutputDto>? Items {get; set;}
        public long? PrePaymentAmount { get;  set; }
        public long? InstalmentAmount { get;  set; }
        public long? InstalmentCount { get;  set; }
        public DateTime? StartDate { get;  set; }
        public DateTime? EndDate { get;  set; }
    }
}
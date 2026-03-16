using housingCooperative.Dtos.CustomerDtos;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Dtos.PlotDtos;

namespace housingCooperative.Dtos.ContractDtos
{
    public class GetPreContractDetailOutputDto
    {

        public string? ProjectId {get; set;}
        public string? ProjectName {get; set;}
        public string? ProjectAddress {get; set;}
        public string? ProjectEngineerName {get; set;}
        public string? ProjectDescription {get; set;}
        public string PlotId { get;  set; }
        public string? PlotName { get;  set; }
        public long? PlotMeterage { get;  set; }
        public string? PlotDescription { get;  set; }
        public long? PlotPrePaymentAmount { get;  set; }
        public long? PlotInstalmentAmount { get;  set; }
        public int? PlotInstalmentCount { get;  set; }
        public string CustomerId { get; set; }
        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }

    }
}
using housingCooperative.Dtos.CustomerDtos;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Dtos.PlotDtos;

namespace housingCooperative.Dtos.ContractDtos
{
    public class GetAllContractsOutputDto
    {
        public GetAllContractsOutputDto(string contractId, string projectId, string? projectName, string plotId, string? plotName, long? meterage)
        {
            ContractId = contractId;
            ProjectId = projectId;
            ProjectName = projectName;
            PlotId = plotId;
            PlotName = plotName;
            PlotMeterage = meterage;
        }

        public string ContractId { get; set; }   
        public string ProjectId { get; set; }   
        public string? ProjectName { get; set; }   
        public string PlotId { get; set; }   
        public string? PlotName { get; set; }   
        public long? PlotMeterage { get; set; }   
        
    }
}
using housingCooperative.Dtos.PhaseDtos;
using housingCooperative.Dtos.PlotDtos;
using housingCooperative.Enums;

namespace housingCooperative.Dtos.LandProjectDtos
{
    public class GetLandProjectDetailOutputDto
    {
        public string? Id { get;  set; }
        public string? Name { get;  set; }
        public string? Address { get;  set; }
        public ProjectTypeEnum? Type { get;  set; }
        public string? EngineerName { get;  set; }
        public DateTime? StartDate { get;  set; }
        public DateTime? EndDate { get;  set; }
        public DateTime? EstimatedStartDate { get;  set; }
        public DateTime? EstimatedEndDate { get;  set; }
        public string? Description { get;  set; }
        public List<string>? ImageList {get; set;}
        public List<GetProjectPhasesOutputDto>? Phases {get; set;}
        public List<GetProjectPlotsOutputDto>? Plots {get; set;}
    }
}
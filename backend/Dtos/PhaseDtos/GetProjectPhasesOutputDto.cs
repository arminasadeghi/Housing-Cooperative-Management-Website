using housingCooperative.Enums;

namespace housingCooperative.Dtos.PhaseDtos
{
    public class GetProjectPhasesOutputDto
    {
        public string? Id { get;  set; }
        public string? Name { get;  set; }
        public int? Order { get;  set; }
        public PhaseStatusEnum? Status { get;  set; }
        public float? Progress { get;  set; }
        public DateTime? StartDate { get;  set; }
        public DateTime? EndDate { get;  set; }
        public DateTime? EstimatedStartDate { get;  set; }
        public DateTime? EstimatedEndDate { get;  set; }
        public List<string>? ImageList { get;  set; }
        public string? Description {get;  set; }
    }
}
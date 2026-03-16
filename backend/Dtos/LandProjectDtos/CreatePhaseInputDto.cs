using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using housingCooperative.Enums;

namespace housingCooperative.Dtos.LandProjectDtos
{
    public class CreatePhaseInputDto
    {
        public string? Name { get; private set; }
        public PhaseStatusEnum? Status { get; private set; }
        public float? Progress { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? EstimatedStartDate { get; private set; }
        public DateTime? EstimatedEndDate { get; private set; }
        public List<string>? ImageIds { get; private set; }
        public string? Description {get; private set; }

        public CreatePhaseInputDto(string? name, PhaseStatusEnum? status, float? progress, DateTime? startDate, DateTime? endDate, DateTime? estimatedStartDate, DateTime? estimatedEndDate, List<string>? imageIds, string? description)
        {
            Name = name;
            Status = status;
            Progress = progress;
            StartDate = startDate;
            EndDate = endDate;
            EstimatedStartDate = estimatedStartDate;
            EstimatedEndDate = estimatedEndDate;
            ImageIds = imageIds;
            Description = description;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using housingCooperative.Domains.Entities;
using housingCooperative.Enums;

namespace housingCooperative.Dtos.LandProjectDtos
{
    public class CreateProjectInputDto
    {
        public string? Name { get; private set; }
        public string? Address { get; private set; }
        public ProjectTypeEnum? Type { get; private set; }
        public List<string>? ImageIds { get; private set; }
        public string? EngineerName { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? EstimatedStartDate { get; private set; }
        public DateTime? EstimatedEndDate { get; private set; }
        public string? Description { get; private set; }
        public List<CreatePlotInputDto> plots { get; private set; }
        public List<CreatePhaseInputDto> phases { get; private set; }

        public CreateProjectInputDto(string? name, string? address, ProjectTypeEnum? type, List<string>? imageIds, string? engineerName, DateTime? startDate, DateTime? endDate, DateTime? estimatedStartDate, DateTime? estimatedEndDate, string? description, List<CreatePlotInputDto> plots, List<CreatePhaseInputDto> phases)
        {
            Name = name;
            Address = address;
            Type = type;
            ImageIds = imageIds;
            EngineerName = engineerName;
            StartDate = startDate;
            EndDate = endDate;
            EstimatedStartDate = estimatedStartDate;
            EstimatedEndDate = estimatedEndDate;
            Description = description;
            this.plots = plots;
            this.phases = phases;
        }
    }
}
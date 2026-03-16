using housingCooperative.Enums;

namespace housingCooperative.Dtos.LandProjectDtos
{
    public class GetAllLandProjectsOutputDto
    {
        public GetAllLandProjectsOutputDto(string? id,string? name, string? address, string? imageId, string? engineerName, string? description, DateTime? startDate, DateTime? endDate, DateTime? estimatedStartDate, DateTime? estimatedEndDate)
        {
            Id = id;
            Name = name;
            Address = address;
            ImageId = imageId;
            EngineerName = engineerName;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            EstimatedStartDate = estimatedStartDate;
            EstimatedEndDate = estimatedEndDate;
        }


        public string? Id { get;  set; }
        public string? Name { get;  set; }
        public string? Address { get;  set; }
        public string? ImageId { get;  set; }
        public string? EngineerName { get;  set; }
        public string? Description { get;  set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? EstimatedStartDate { get; private set; }
        public DateTime? EstimatedEndDate { get; private set; }
    }
}
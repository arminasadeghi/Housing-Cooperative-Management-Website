using housingCooperative.Enums;
using Library.DDD.Core.BaseModels;

namespace housingCooperative.Domains.Entities
{
    public class PhaseEntity : BaseEntity<string>, ICreatedDateTime, IModifiedDateTime
    {
        public string ProjectId { get; private set; }
        public string? Name { get; private set; }
        public int? Order { get; private set; }
        public PhaseStatusEnum? Status { get; private set; }
        public float? Progress { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? EstimatedStartDate { get; private set; }
        public DateTime? EstimatedEndDate { get; private set; }
        public string? ImageIds { get; private set; }
        public string? Description {get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public LandProjectEntity? LandProject { get; private set; }
        public PhaseEntity(string? name, string projectId, int? order, PhaseStatusEnum? status, float? progress, DateTime? startDate, DateTime? endDate, DateTime? estimatedStartDate, DateTime? estimatedEndDate, string? imageIds, string? description)
        {
            initEntity();
            Name = name;
            ProjectId = projectId;
            Order = order;
            Status = status;
            Progress = progress;
            StartDate = startDate;
            EndDate = endDate;
            EstimatedStartDate = estimatedStartDate;
            EstimatedEndDate = estimatedEndDate;
            ImageIds = imageIds;
            Description = description;
        }
        private PhaseEntity(){}
        public List<string>? ImageList 
        { 
            get
            { 
                return 
                String.IsNullOrEmpty(this.ImageIds) 
                ? new List<string>() 
                : ImageIds
                    .Split(",")
                    .Select(x => "https://housingcooperative-storage.darkube.app/images/" + x + ".JPG")
                    .ToList();
            } 
            private set
            { 
                ImageList = value ;
                ImageIds = string.Join<string>("," , value.Select(x => (x).ToString())); 
            }
        }
        public void initEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            IsDeleted = false;
            IsVisibled = true;
        }

        public void Modify()
        {
            this.ModifiedAt = DateTime.Now;
        }

        public void Delete()
        {
            this.IsDeleted = true;
        }

        public void Disabled()
        {
            this.IsVisibled = false;
        }
    }
}
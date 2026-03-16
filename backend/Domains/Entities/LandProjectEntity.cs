using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Enums;
using Library.DDD.Core.BaseModels;

namespace housingCooperative.Domains.Entities
{
    public class LandProjectEntity : BaseEntity<string>, ICreatedDateTime, IModifiedDateTime
    {
        
        public string? Name { get; private set; }
        public string? Address { get; private set; }
        public ProjectTypeEnum? Type { get; private set; }
        public string? ImageIds { get; private set; }
        public string? EngineerName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? EstimatedStartDate { get; private set; }
        public DateTime? EstimatedEndDate { get; private set; }
        public string? Description { get; private set; }
        private List<PhaseEntity> _phases = new List<PhaseEntity>();
        public IEnumerable<PhaseEntity> Phases => _phases;
        private List<PlotEntity> _plots = new List<PlotEntity>();
        private LandProjectEntity()
        {
            
        }
        public LandProjectEntity(CreateProjectInputDto dto)
        {

            initEntity();
            Name = dto.Name ;
            Address = dto.Address ;
            Type = dto.Type ;
            ImageIds = string.Join("," , dto.ImageIds);
            EngineerName = dto.EngineerName ;
            StartDate = dto.StartDate ;
            EndDate = dto.EndDate ;
            EstimatedEndDate = dto.EstimatedEndDate ;
            EstimatedStartDate = dto.EstimatedStartDate ;
            Description = dto.Description ;

            var order = 0;
            dto.phases.ForEach(x =>
            {
                 var phase = new PhaseEntity
                 (
                        x.Name ,
                        this.Id ,
                        order ++ ,
                        x.Status ,
                        x.Progress ,
                        x.StartDate ,
                        x.EndDate ,
                        x.EstimatedStartDate ,
                        x.EstimatedEndDate ,
                        string.Join("," , x.ImageIds) ,
                        x.Description

                 );

                 this._phases.Add(phase);
            });

            dto.plots.ForEach(x =>
            {
                
                var plot = new PlotEntity
                (
                    x.Name , 
                    this.Id ,
                    x.Meterage ,
                    x.Value , 
                    x.PrePaymentAmount ,
                    x.InstalmentAmount , 
                    x.Description
                );

                this._plots.Add(plot);
            });
        }
        public IEnumerable<PlotEntity> Plots => _plots;
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
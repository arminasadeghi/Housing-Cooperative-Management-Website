using Library.DDD.Core.BaseModels;

namespace housingCooperative.Domains.Entities
{
    public class PlotEntity : BaseEntity<string>, ICreatedDateTime, IModifiedDateTime
    {
        public string ProjectId { get; private set; }
        public string Name { get; private set; }

        public PlotEntity(string name, string projectId, long? meterage, long? value, long? prePaymentAmount, long? instalmentAmount, string? description)
        {
            initEntity();
            Name = name;
            ProjectId = projectId;
            Meterage = meterage;
            Value = value;
            PrePaymentAmount = prePaymentAmount;
            InstalmentAmount = instalmentAmount;
            Description = description;
        }

        public long? Meterage { get; private set; }
        public long? Value { get; private set; }
        public long? PrePaymentAmount { get; private set; }
        public long? InstalmentAmount { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public LandProjectEntity? LandProject { get; private set; }
        public string? ContractId {get; private set;}
        public ContractEntity? Contract {get; private set;}
        public int? InstalmentCount
        { 
            get
            { 
                return CalculateInstalmentCount();
            } 
            private set
            { 
                InstalmentCount = value ;
            }
        }

        private PlotEntity(){}
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
        public void SetContract(string contractId)
        {
            this.ContractId = contractId;
        }

        public int? CalculateInstalmentCount()
        {
            if(this.Value <= 0 || this.InstalmentAmount <= 0 || Value - PrePaymentAmount <= 0)
                return 0;
            else 
                return Convert.ToInt32((Value - PrePaymentAmount)/InstalmentAmount);
        }
    }
}
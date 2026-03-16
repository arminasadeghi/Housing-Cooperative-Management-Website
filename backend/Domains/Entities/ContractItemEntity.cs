using Library.DDD.Core.BaseModels;

namespace housingCooperative.Domains.Entities
{
    public class ContractItemEntity : BaseEntity<string>, ICreatedDateTime, IModifiedDateTime
    {


        public ContractItemEntity(string contractId, long? instalmentAmount, DateTime? dueDate)
        {
            this.Id = Guid.NewGuid().ToString();
            
            ContractId = contractId;
            InstalmentAmount = instalmentAmount;
            PaidAmount = 0;
            DueDate = dueDate;
            CreatedAt = DateTime.Now ;
        }
        public string ContractId { get; private set; }

        public long? InstalmentAmount { get; private set; }
        public long? PaidAmount { get; private set; }
        public long? RemainAmount => InstalmentAmount - PaidAmount ; 
        public DateTime? DueDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public ContractEntity? Contract { get; private set;}
        private List<ContractPaidItemEntity> _paidItems = new List<ContractPaidItemEntity>();
        public IEnumerable<ContractPaidItemEntity> PaidItems => _paidItems;
        public ContractItemEntity(){}
        public void initEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            IsDeleted = false;
            IsVisibled = true;
        }
        public void Pay(long? shouldPay)
        {
            this.PaidAmount += shouldPay ;
            Modify();
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
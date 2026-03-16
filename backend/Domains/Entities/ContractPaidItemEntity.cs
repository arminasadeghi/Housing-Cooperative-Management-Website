using Library.DDD.Core.BaseModels;

namespace housingCooperative.Domains.Entities
{
    public class ContractPaidItemEntity : BaseEntity<string>, ICreatedDateTime, IModifiedDateTime
    {
        public string ContractItemId { get; private set; }
        public long? Amount { get; private set; }
        public string PaymentId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public ContractItemEntity? ContractItem { get; private set; }
        public ContractPaidItemEntity(string contractItemId, long? amount, string paymentId)
        {
            initEntity();
            ContractItemId = contractItemId;
            Amount = amount;
            PaymentId = paymentId;
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
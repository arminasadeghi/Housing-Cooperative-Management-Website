using DocumentFormat.OpenXml.Drawing;
using Library.DDD.Core.BaseModels;

namespace housingCooperative.Domains.Entities
{
    public class ContractEntity : BaseEntity<string>, ICreatedDateTime, IModifiedDateTime
    {


        public  ContractEntity(string customerId, string plotId, string projectId, long? totalCommitedAmount, long? totalPaidAmount, long? prePaymentAmount, long? instalmentAmount, string? description)
        {
            this.Id = Guid.NewGuid().ToString();
            CustomerId = customerId;
            PlotId = plotId;
            ProjectId = projectId;
            TotalCommitedAmount = totalCommitedAmount;
            TotalPaidAmount = totalPaidAmount;
            PrePaymentAmount = prePaymentAmount;
            InstalmentAmount = instalmentAmount;
            Description = description;

            CreatedAt = DateTime.Now ;
            StartDate = DateTime.Now ;

            createItems();
        }
        private void createItems()
        {
            
            var remainAmount = TotalCommitedAmount - PrePaymentAmount ;
            var date = this.StartDate ;
            while(remainAmount > 0 )
            {   

              var instalmentAmount = remainAmount - this.InstalmentAmount > 0 ? this.InstalmentAmount : remainAmount ; 
              date = date.Value.AddMonths(1);
              remainAmount = remainAmount - instalmentAmount ;

              var item = new ContractItemEntity(this.Id , instalmentAmount , date );
              this._items.Add(item);
            
            }

            this.EndDate = this.Items.OrderByDescending(x => x.DueDate).First().DueDate ; 
        }
        public string CustomerId { get; private set; }

        public string PlotId { get; private set; }
        public string ProjectId { get; private set; }
        public long? TotalCommitedAmount { get; private set; }
        public long? TotalPaidAmount { get; private set; }
        public long? PrePaymentAmount { get; private set; }
        public long? InstalmentAmount { get; private set; }
        public string? Description { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public CustomerEntity? Customer { get; private set; }
        public PlotEntity? Plot { get; private set; }
        public LandProjectEntity? LandProject { get; private set; }
        private List<ContractItemEntity> _items = new List<ContractItemEntity>();
        public IEnumerable<ContractItemEntity> Items => _items;
        public ContractEntity(){}
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


        public List<string> Pay(long? payAmount)
        {
            var remailTotalPaiedMoney = payAmount ;
            var paidItemsId = new List<string>();

            this._items.OrderBy(x => x.DueDate).ToList().ForEach(x =>
            {

                if(remailTotalPaiedMoney == 0  || x.RemainAmount == 0)
                  return;    

                long? shouldPay = remailTotalPaiedMoney - x.RemainAmount > 0 ? x.RemainAmount : remailTotalPaiedMoney ;

                remailTotalPaiedMoney -= shouldPay ;

                x.Pay(shouldPay);
                paidItemsId.Add(x.Id);

            });
            
            Modify();

            return paidItemsId ; 
        }

    }
}
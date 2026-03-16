namespace housingCooperative.Dtos.ContractDtos
{
    public class GetAllContrcatItemsOutputDto
    {
        public string? Id {get; set;}
        public long? InstalmentAmount { get;  set; }
        public long? PaidAmount { get;  set; }
        public long? RemainAmount => InstalmentAmount - PaidAmount ; 
        public DateTime? DueDate { get;  set; }
    }
}
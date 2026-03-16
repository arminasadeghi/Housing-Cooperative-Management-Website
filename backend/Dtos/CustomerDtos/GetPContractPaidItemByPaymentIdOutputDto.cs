namespace housingCooperative.Dtos.CustomerDtos
{
    public class GetPContractPaidItemByPaymentIdOutputDto
    {
        public string ContractItemId { get;  set; }
        public long? Amount { get;  set; }
        public string PaymentId { get;  set; }
        // Aggregate az contractItem
        public DateTime? DueDate { get;  set; }
    }
}
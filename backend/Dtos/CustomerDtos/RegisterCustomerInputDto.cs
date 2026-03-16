namespace housingCooperative.Dtos.CustomerDtos
{
    public class RegisterCustomerInputDto
    {
        public RegisterCustomerInputDto(string customerId, string phoneNumber)
        {
            CustomerId = customerId;
            PhoneNumber = phoneNumber;
        }

        public string CustomerId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
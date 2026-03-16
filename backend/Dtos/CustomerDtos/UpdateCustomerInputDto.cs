using housingCooperative.Enums;

namespace housingCooperative.Dtos.CustomerDtos
{
    public class UpdateCustomerInputDto
    {
        public string? FirstName { get;  set; }
        public string? LastName { get;  set; }
        public string? NationalId { get;  set; }
        public DateTime? BirthDate { get;  set; }
        public GenderEnum? Gender { get;  set; }
        public string? JobTitle { get;  set; }
        public string? City { get;  set; }
        public string? Address { get;  set; }
    }
}
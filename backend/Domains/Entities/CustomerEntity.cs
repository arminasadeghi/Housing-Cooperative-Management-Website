using housingCooperative.Enums;
using Library.DDD.Core.BaseModels;

namespace housingCooperative.Domains.Entities
{
    public class CustomerEntity : BaseEntity<string>, ICreatedDateTime, IModifiedDateTime
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? NationalId { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public GenderEnum? Gender { get; private set; }
        public string? JobTitle { get; private set; }
        public string? City { get; private set; }
        public string? Address { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public string? Description {get; private set; }
        private List<ContractEntity> _contracts = new List<ContractEntity>();
        public IEnumerable<ContractEntity> Contracts => _contracts;
        
        public CustomerEntity(){}
        
        public CustomerEntity (string id, string phoneNumber)
        {
            initEntityWithId(id);
            PhoneNumber = phoneNumber;
        }
        public CustomerEntity(string? firstName, string? lastName, string phoneNumber, string? natinalId)
        {
            initEntity();
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            NationalId = natinalId;

        }
        public CustomerEntity(string Id, string? firstName, string? lastName, string phoneNumber,string? natinalId)
        {
            initEntityWithId(Id);
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            NationalId = natinalId;
        }

        public void Update (string? firstName, string? lastName, string? natinalId,
         DateTime? birthDate, GenderEnum? gender, string? jobTitle, string? city, string? address)
        {
            FirstName = firstName;
            LastName = lastName;
            NationalId =  natinalId;
            BirthDate = birthDate;
            Gender = gender;
            JobTitle = jobTitle;
            City = city;
            Address = address;

            this.Modify();
        }
        public void initEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            IsDeleted = false;
            IsVisibled = true;
        }
        public void initEntityWithId(string id)
        {
            Id = id;
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
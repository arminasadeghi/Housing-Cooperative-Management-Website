namespace housingCooperative.Dtos.PlotDtos
{
    public class GetProjectPlotsOutputDto
    {
        public string? Id { get;  set; }
        public string? Name { get;  set; }
        public long? Meterage { get;  set; }
        public long? Value { get;  set; }
        public long? PrePaymentAmount { get;  set; }
        public long? InstalmentAmount { get;  set; }
        public int? InstalmentCount { get;  set; }
        public string? Description { get;  set; }
    }
}
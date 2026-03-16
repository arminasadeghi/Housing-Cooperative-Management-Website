using housingCooperative.Commands.CustomerCommands;
using housingCooperative.Domains.Entities;
using housingCooperative.Dtos.CustomerDtos;
using housingCooperative.Dtos.LandProjectDtos;
using housingCooperative.Dtos.PhaseDtos;
using housingCooperative.Dtos.PlotDtos;

namespace housingCooperative.MapsterConfiguration
{
    public class MapsterConfiguration
    {
        public void Register(TypeAdapterConfig config)
        {

            TypeAdapterConfig<RegisterCustomerInputDto, CreateCustomerByIdentityCommand>
                .NewConfig()
                .ConstructUsing(x => new CreateCustomerByIdentityCommand(x.CustomerId , x.PhoneNumber));




            TypeAdapterConfig<UpdateCustomerInputDto , UpdateCustomerByUserCommand>
            .NewConfig()
            .Map(d=>d.FirstName , s=>s.FirstName)
            .Map(d=>d.LastName , s=>s.LastName)
            .Map(d=>d.NationalId , s=>s.NationalId)
            .Map(d=>d.BirthDate , s=>s.BirthDate)
            .Map(d=>d.Gender , s=>s.Gender)
            .Map(d=>d.JobTitle , s=>s.JobTitle)
            .Map(d=>d.City , s=>s.City)
            .Map(d=>d.Address , s=>s.Address);


            TypeAdapterConfig<CustomerEntity , GetCustomerInfoOutputDto>
            .NewConfig()
            .Map(d=>d.Id , s=>s.Id)
            .Map(d=>d.FirstName , s=>s.FirstName)
            .Map(d=>d.LastName , s=>s.LastName)
            .Map(d=>d.PhoneNumber , s=>s.PhoneNumber)
            .Map(d=>d.NationalId , s=>s.NationalId)
            .Map(d=>d.BirthDate , s=>s.BirthDate)
            .Map(d=>d.Gender , s=>s.Gender)
            .Map(d=>d.JobTitle , s=>s.JobTitle)
            .Map(d=>d.City , s=>s.City)
            .Map(d=>d.Address , s=>s.Address);


            TypeAdapterConfig<CreateCustomerByIdentityCommand , CustomerEntity>
            .NewConfig()
            .Map(d=>d.Id , s=>s.CustomerId)
            .Map(d=>d.PhoneNumber , s=>s.PhoneNumber);

            TypeAdapterConfig<LandProjectEntity , GetLandProjectDetailOutputDto>
            .NewConfig()
            .Map(d=>d.Id , s=>s.Id)
            .Map(d=>d.Name , s=>s.Name)
            .Map(d=>d.Address , s=>s.Address)
            .Map(d=>d.Type , s=>s.Type)
            .Map(d=>d.ImageList , s=>s.ImageList)
            .Map(d=>d.EngineerName , s=>s.EngineerName)
            .Map(d=>d.StartDate , s=>s.StartDate)
            .Map(d=>d.EndDate , s=>s.EndDate)
            .Map(d=>d.EstimatedStartDate , s=>s.EstimatedStartDate)
            .Map(d=>d.EstimatedEndDate , s=>s.EstimatedEndDate)
            .Map(d=>d.Description , s=>s.Description)
            .Map(d=>d.Phases , s=> s.Phases)
            .Map(d=>d.Plots , s=>s.Plots);

            TypeAdapterConfig<PhaseEntity , GetProjectPhasesOutputDto>
            .NewConfig()
            .Map(d=>d.Id , s=>s.Id)
            .Map(d=>d.Name , s=>s.Name)
            .Map(d=>d.Order , s=>s.Order)
            .Map(d=>d.Status , s=>s.Status)
            .Map(d=>d.Progress , s=>s.Progress)
            .Map(d=>d.StartDate , s=>s.StartDate)
            .Map(d=>d.EndDate , s=>s.EndDate)
            .Map(d=>d.EstimatedStartDate , s=>s.EstimatedStartDate)
            .Map(d=>d.EstimatedStartDate , s=>s.EstimatedStartDate)
            .Map(d=>d.EstimatedEndDate , s=>s.EstimatedEndDate)
            .Map(d=>d.ImageList , s=>s.ImageList)
            .Map(d=>d.Description , s=>s.Description);

            TypeAdapterConfig<PlotEntity , GetProjectPlotsOutputDto>
            .NewConfig()
            .Map(d=>d.Id , s=>s.Id)
            .Map(d=>d.Name , s=>s.Name)
            .Map(d=>d.Meterage , s=>s.Meterage)
            .Map(d=>d.Value , s=>s.Value)
            .Map(d=>d.PrePaymentAmount , s=>s.PrePaymentAmount)
            .Map(d=>d.InstalmentAmount , s=>s.InstalmentAmount)
            .Map(d=>d.InstalmentCount , s=>s.InstalmentCount)
            .Map(d=>d.Description , s=>s.Description);
        }
    }
}
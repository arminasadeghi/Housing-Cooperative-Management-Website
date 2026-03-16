using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace housingCooperative.Dtos.LandProjectDtos
{
    public class CreatePlotInputDto
    {
        public string Name { get; private set; }
        public long? Meterage { get; private set; }
        public long? Value { get; private set; }
        public long? PrePaymentAmount { get; private set; }
        public long? InstalmentAmount { get; private set; }
        public string? Description { get; private set; }

        public CreatePlotInputDto(string name, long? meterage, long? value, long? prePaymentAmount, long? instalmentAmount, string? description)
        {
            Name = name;
            Meterage = meterage;
            Value = value;
            PrePaymentAmount = prePaymentAmount;
            InstalmentAmount = instalmentAmount;
            Description = description;
        }
    }
}
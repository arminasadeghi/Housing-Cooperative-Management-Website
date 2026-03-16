using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using housingCooperative.Dtos.CustomerDtos;
using housingCooperative.Dtos.Enums;

namespace housingCooperative.Dtos.PaymentDtos
{
    public class GetCustomerPaymentsDto
    {


        public string Id { get; set; }
        public long? Amount { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public PaymentTypeEnum Type { get; set; }
        public DateTime CreatedAt { get; set; }
        
        
    }

    public class GetPaymentDetailDto : GetCustomerPaymentsDto
    {
        
        public string ProjectName { get; set; }
        public string PlotName { get; set; }
        
        
    }

    public class GetPaymentDetailByAdminDto : GetPaymentDetailDto
    {

        public GetCustomerInfoOutputDto CustomerInfo { get; set; }
        
        
    }
    
}
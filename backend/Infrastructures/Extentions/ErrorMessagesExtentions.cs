
namespace housingCooperative.Infrastructures.Extentions
{
    public static class ErrorMessagesExtentions
    {
         public static int GetCode(this Errors error) 
         {
             return error.GetEnumAttribute<ErrorDescriptionAttribute>().Code ;
         }
         public static string GetPersionMessage(this Errors error) 
         {
             return error.GetEnumAttribute<ErrorDescriptionAttribute>().PersionMessage ;
         }
         public static string GetMessage(this Errors error) 
         {
             return error.ToString();
         }
         public static List<ErrorDto> GetErrors()    
         {

            List<ErrorDto> errorDto = new List<ErrorDto>();
            typeof(Errors).GetEnumValues().OfType<Errors>().ToList().ForEach( x => 
            {
                var ErrorDescription = x.GetEnumAttribute<ErrorDescriptionAttribute>();
                errorDto.Add(new ErrorDto()
                {
                    Code = ErrorDescription.Code ,
                    ErrorMessage = x.ToString(),
                    PersionDescription = ErrorDescription.PersionMessage
                });
            });

            return errorDto ;

         }
    }


    class ErrorDescriptionAttribute : Attribute
    {
            public int Code { get; private set; }
            public string PersionMessage { get; private set; }

            public ErrorDescriptionAttribute(int code, string persionMessage)
            {
                Code = code;
                PersionMessage = persionMessage;
            }

    }

    
}
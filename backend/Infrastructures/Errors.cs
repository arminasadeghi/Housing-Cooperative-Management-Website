
namespace housingCooperative.Infrastructures
{
    
    
    public enum Errors
    {
            
        [ErrorDescriptionAttribute(409 , "کاربر مورد نظر موجود میباشد")] UserAlreadyExist  ,
        [ErrorDescriptionAttribute(400 , "شماره تلفن معتبر نمیباشد")] InvalidPhoneNumber  ,
        [ErrorDescriptionAttribute(404 , "کاربر مورد نظر یافت نشد")] CustomerNotFound  ,
        [ErrorDescriptionAttribute(404 , "قطعه مورد نظر قرارداد دارد")] PlotIsNotFree  ,
        [ErrorDescriptionAttribute(404 , "پروژه مورد نظر یافت نشد")] ProjectNotFound  ,
        [ErrorDescriptionAttribute(404 , "قرارداد مورد نظر یافت نشد")] ContractNotFound  ,
        [ErrorDescriptionAttribute(404 , "کاربر در این پروژه مشارکت ندارد")] CustomerProjectNotFound  ,
        [ErrorDescriptionAttribute(404 , "اطلاعات ناقص است")] RequiredItemIsNull  ,
        [ErrorDescriptionAttribute(404 , "خطایی در بروزرسانی کاربر رخ داده است")] CustomerUpdateFailed  ,
        [ErrorDescriptionAttribute(400 , "کد ملی معتبر نمی باشد")] InvalidNationalCode  ,
        [ErrorDescriptionAttribute(400 , " تاریخ تولد معتبر نمیباشد")] InvalidBirthDate  ,
        [ErrorDescriptionAttribute(400 , " پلات یافت نشد")] PlotNotFound,
        [ErrorDescriptionAttribute(400 , " آیتمی یافت نشد")] NoItemExistForPay,
    }

    

}
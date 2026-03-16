using System.Text.RegularExpressions;

namespace housingCooperative.Infrastructures.Extentions
{
    public static class StringExtentions
    {
        public static bool IsPhoneNumberValid(this string number) => Regex.Match(number, @"^09\d{9}$").Success;

        public static bool IsNationalIdValid(this string nationalId) => Regex.Match(nationalId , @"^[0-9]{10}$").Success;
    }
}
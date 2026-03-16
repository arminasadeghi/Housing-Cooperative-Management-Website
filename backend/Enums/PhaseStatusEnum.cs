using System.ComponentModel;

namespace housingCooperative.Enums
{
    public enum PhaseStatusEnum
    {
        [Description("شروع نشده")]
        NotStarted = 0,
        [Description("شروع شده")]
        Started = 1, 
        [Description("پایان یافته")]
        Ended = 2,
        
    }
}
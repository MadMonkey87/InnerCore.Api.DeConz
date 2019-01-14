using System;

namespace InnerCore.Api.DeConz.Models
{
    [Flags]
    public enum RecurringDay
    {
        RecurringNone = 0,
        RecurringSunday = 1,
        RecurringSaturday = 2,
        RecurringFriday = 4,
        RecurringThursday = 8,
        RecurringWednesday = 16,
        RecurringTuesday = 32,
        RecurringMonday = 64,
        RecurringWeekdays = 124,
        RecurringWeekend = 3,
        RecurringAlldays = 127,
    }
}
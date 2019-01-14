using System;

namespace InnerCore.Api.deConz.Models
{
    public class DeConzDateTime
    {
        /// <summary>
        /// Absolute time
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Timers and timeparts for recurring times
        /// </summary>
        public TimeSpan? TimerTime { get; set; }

        /// <summary>
        /// Randomized time
        /// </summary>
        public TimeSpan? RandomizedTime { get; set; }

        /// <summary>
        /// Recurring days
        /// </summary>
        public RecurringDay RecurringDay { get; set; }

        /// <summary>
        /// Number of recurrences (0=repeat forever)
        /// </summary>
        public int? NumberOfRecurrences { get; set; }
    }
}

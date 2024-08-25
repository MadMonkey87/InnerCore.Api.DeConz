using System.Runtime.Serialization;

namespace InnerCore.Api.DeConz.Models.Rules
{
    /// <summary>
    /// Possible light alerts
    /// </summary>
    public enum RuleOperator
    {
        /// <summary>
        /// Equal, Used for bool and int.
        /// </summary>
        [EnumMember(Value = "eq")]
        Equal,

        /// <summary>
        /// Time (timestamps) int and bool values. Only dx or ddx is allowed, but not both. Triggers when value of button event is changed or change of presence is detected.ddx is introduced in 1.13
        /// </summary>
        [EnumMember(Value = "dx")]
        Dx,

        /// <summary>
        /// Time (timestamps) int and bool values. Only dx or ddx is allowed, but not both. Triggers when value of button event is changed or change of presence is detected.ddx is introduced in 1.13
        /// </summary>
        [EnumMember(Value = "ddx")]
        Ddx,

        /// <summary>
        /// Time (timestamps) int and bool values.  An attribute has or has not changed for a given time.  Does not trigger a rule change.  Not allowed on /config/utc and /config/localtime. Introduced in 1.13
        /// </summary>
        [EnumMember(Value = "stable")]
        Stable,

        /// <summary>
        /// Time (timestamps) int and bool values.  An attribute has or has not changed for a given time.  Does not trigger a rule change.  Not allowed on /config/utc and /config/localtime. Introduced in 1.13
        /// </summary>
        [EnumMember(Value = "not stable")]
        NotStable,

        /// <summary>
        /// Current time is in or not in given time interval.  "in" rule will be triggered on starttime and "not in" rule will be triggered on endtime.  Only one "in" operator is allowed in a rule.  Multiple "not in" operators are allowed in a rule.  Not allowed to be combined with "not in". Introduced in  1.14
        /// </summary>
        [EnumMember(Value = "in")]
        In,

        /// <summary>
        /// Current time is in or not in given time interval.  "in" rule will be triggered on starttime and "not in" rule will be triggered on endtime.  Only one "in" operator is allowed in a rule.  Multiple "not in" operators are allowed in a rule.  Not allowed to be combined with "not in". Introduced in  1.14
        /// </summary>
        [EnumMember(Value = "not in")]
        NotIn,

        /// <summary>
        /// LessThan, Allowed on int values
        /// </summary>
        [EnumMember(Value = "lt")]
        LessThan,

        /// <summary>
        /// GreaterThan, Allowed on int values
        /// </summary>
        [EnumMember(Value = "gt")]
        GreaterThan,
    }
}

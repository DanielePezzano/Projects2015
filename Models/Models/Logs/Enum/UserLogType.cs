using System.Runtime.Serialization;
namespace Models.Logs.Enum
{
    [DataContract]
    public enum UserLogType
    {
        [EnumMember]
        Registration,
        [EnumMember]
        PasswordReset,
        [EnumMember]
        Donations,
        [EnumMember]
        TechnologyCreated,
        [EnumMember]
        ShipClassCreated,
        [EnumMember]
        FleetAssembled,
        [EnumMember]
        LoginFailed,
        [EnumMember]
        LoginSuccess,
        [EnumMember]
        HolidayPeriodStarted,
        [EnumMember]
        HolidayPeriodEnded
    }
}
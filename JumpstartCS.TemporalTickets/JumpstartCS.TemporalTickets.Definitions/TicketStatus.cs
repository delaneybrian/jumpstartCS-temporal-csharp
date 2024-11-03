using System.Runtime.Serialization;

namespace JumpstartCS.TemporalTickets.Definitions
{
    [DataContract]
    public enum TicketStatus
    {
        [EnumMember]
        Default = 0,

        [EnumMember]
        Unassigned = 1,

        [EnumMember]
        Reserved = 2,

        [EnumMember]
        Confirmed = 3,

        [EnumMember]
        Cancelled = 4
    }
}

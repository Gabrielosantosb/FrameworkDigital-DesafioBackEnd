using System.Runtime.Serialization;

namespace FrameworkDigital_DesafioBackEnd.ORM.Enum
{    
     public enum LeadStatusEnum
    {
        [EnumMember(Value = "Invited")]
        Invited = 0,

        [EnumMember(Value = "Accepted")]
        Accepted = 1,

        [EnumMember(Value = "Declined")]
        Declined = 2
      
    }
}

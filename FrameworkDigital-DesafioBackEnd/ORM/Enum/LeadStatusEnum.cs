using System.Runtime.Serialization;

namespace FrameworkDigital_DesafioBackEnd.ORM.Enum
{    
     public enum LeadStatusEnum
    {
        [EnumMember(Value = "Invited")]
        Invited = 1,

        [EnumMember(Value = "Accepted")]
        Accepted = 2,

        [EnumMember(Value = "Declined")]
        Declined = 3
      
    }
}

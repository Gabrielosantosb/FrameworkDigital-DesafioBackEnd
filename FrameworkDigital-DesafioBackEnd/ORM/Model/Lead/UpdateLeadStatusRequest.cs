using FrameworkDigital_DesafioBackEnd.ORM.Enum;

namespace FrameworkDigital_DesafioBackEnd.ORM.Model.Lead
{
    public class UpdateLeadStatusRequest
    {
        public LeadStatusEnum Status { get; set; }
    }
}

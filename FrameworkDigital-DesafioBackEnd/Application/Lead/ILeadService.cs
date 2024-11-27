using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;

namespace FrameworkDigital_DesafioBackEnd.Application.Lead
{
    public interface ILeadService
    {
        IEnumerable<LeadModel> GetLeads();
        LeadModel GetLeadById(int leadId);
        LeadModel CreateLead(LeadRequest leadRequest);
        LeadModel UpdateLead(int leadId, LeadRequest updatedUnit);
        bool UpdateLeadStatus(int leadId, string status);


    }
}

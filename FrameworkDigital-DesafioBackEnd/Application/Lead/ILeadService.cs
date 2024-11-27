using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;

namespace FrameworkDigital_DesafioBackEnd.Application.Lead
{
    public interface ILeadService
    {
        IEnumerable<LeadModel> GetLeads(
            int page = 1,
            int pageSize = 10);
        LeadModel GetLeadById(int leadId);
        LeadModel CreateLead(LeadRequest leadRequest);
        LeadModel UpdateLead(int leadId, LeadRequest updatedLead);
        bool UpdateLeadStatus(int leadId, string status);


    }
}

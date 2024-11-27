using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;

namespace FrameworkDigital_DesafioBackEnd.Application.Lead
{
    public interface ILeadService
    {
        IEnumerable<LeadModel> GetLeads();
        LeadModel CreateLead(CreateLeadRequest leadRequest);

        LeadModel UpdateLead(int id, LeadModel updatedUnit);
        LeadModel DesativeLead(int leadId);

    }
}

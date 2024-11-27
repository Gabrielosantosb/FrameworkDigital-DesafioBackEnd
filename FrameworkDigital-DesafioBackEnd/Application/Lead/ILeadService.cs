using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Pagination;

namespace FrameworkDigital_DesafioBackEnd.Application.Lead
{
    public interface ILeadService
    {
        IEnumerable<LeadModel> GetLeads(PaginationDTO pagination, GetLeadsFilterDTO leadsFilter);
        LeadModel GetLeadById(int leadId);
        LeadModel CreateLead(LeadRequest leadRequest);
        LeadModel UpdateLead(int leadId, LeadRequest updatedLead);
        bool UpdateLeadStatus(int leadId, UpdateLeadStatusRequest statusRequest);


    }
}

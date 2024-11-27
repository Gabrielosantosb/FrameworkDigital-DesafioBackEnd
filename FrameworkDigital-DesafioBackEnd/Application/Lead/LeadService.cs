using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;

namespace FrameworkDigital_DesafioBackEnd.Application.Lead
{
    public class LeadService : ILeadService
    {
        public IEnumerable<LeadModel> GetLeads()
        {
            throw new NotImplementedException();
        }

        public LeadModel GetLeadById(int leadId)
        {
            throw new NotImplementedException();
        }

        public LeadModel CreateLead(LeadRequest leadRequest)
        {
            throw new NotImplementedException();
        }
            
        public LeadModel UpdateLead(int id, LeadModel updatedUnit)
        {
            throw new NotImplementedException();
        }

        public LeadModel DesativeLead(int leadId)
        {
            throw new NotImplementedException();
        }

        public LeadModel UpdateLead(int leadId, LeadRequest updatedUnit)
        {
            throw new NotImplementedException();
        }

        public bool UpdateLeadStatus(int leadId, string status)
        {
            throw new NotImplementedException();
        }
    }
}

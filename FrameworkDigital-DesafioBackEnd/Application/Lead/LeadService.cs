using FrameworkDigital_DesafioBackEnd.ORM.Context;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace FrameworkDigital_DesafioBackEnd.Application.Lead
{
    public class LeadService : ILeadService
    {
        private readonly BaseRepository<LeadModel> _leadRepository;
        private readonly FrameworkDigitalDbContext _context;
        private readonly IMapper _mapper;

        public LeadService(BaseRepository<LeadModel> leadRepository, FrameworkDigitalDbContext context, IMapper mapper)
        {
            _leadRepository = leadRepository;
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<LeadModel> GetLeads()
        {
            throw new NotImplementedException();
        }

        public LeadModel GetLeadById(int leadId)
        {
            var lead = _leadRepository._context
                .Lead
                .FirstOrDefault(lead => lead.LeadId == leadId);
            return lead;
        }

        public LeadModel CreateLead(LeadRequest leadRequest)
        {
            if (leadRequest == null) return null;
            var newLead = _mapper.Map<LeadModel>(leadRequest);
            var res = _leadRepository.Add(newLead);
            _leadRepository.SaveChanges();
            return res;
        }

        public LeadModel UpdateLead(int leadId, LeadRequest updatedLead)
        {
            var existingLead = _leadRepository.GetById(leadId);
            if (existingLead == null) return null;
       
            _mapper.Map(updatedLead, existingLead);

            _leadRepository.SaveChanges();

            return existingLead;
        }

        public bool UpdateLeadStatus(int leadId, string status)
        {
            throw new NotImplementedException();
        }

        
    }
}

using FrameworkDigital_DesafioBackEnd.ORM.Context;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Pagination;
using FrameworkDigital_DesafioBackEnd.ORM.Enum;


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

        public IEnumerable<LeadModel> GetLeads(PaginationDTO pagination, GetLeadsFilterDTO filter)
        {
            var query = _leadRepository._context.Lead.AsQueryable();            
            query = ApplyGetLeadsFilters(query, filter);            
            query = query.Skip((pagination.Page - 1) * pagination.PageSize)
                         .Take(pagination.PageSize);

            return query.ToList();
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

        public bool UpdateLeadStatus(int leadId, UpdateLeadStatusRequest statusRequest)
        {
            if (!IsValidLeadStatus(statusRequest.Status))
            {
                return false;
            }

            var lead = _leadRepository.GetById(leadId);
            if (lead == null || statusRequest == null)
            {
                return false;
            }

            // Verifica e aplica desconto
            HasDiscount(lead, statusRequest.Status);

            lead.Status = statusRequest.Status;

            _leadRepository.SaveChanges();

            return true;
        }

        private IQueryable<LeadModel> ApplyGetLeadsFilters(IQueryable<LeadModel> query, GetLeadsFilterDTO filter)
        {
            if (!string.IsNullOrEmpty(filter.ContactFirstName))
                query = query.Where(lead => lead.ContactFirstName.Contains(filter.ContactFirstName));

            if (!string.IsNullOrEmpty(filter.ContactLastName))
                query = query.Where(lead => lead.ContactLastName.Contains(filter.ContactLastName));

            if (!string.IsNullOrEmpty(filter.ContactEmail))
                query = query.Where(lead => lead.ContactEmail.Contains(filter.ContactEmail));

            if (!string.IsNullOrEmpty(filter.Suburb))
                query = query.Where(lead => lead.Suburb.Contains(filter.Suburb));

            if (!string.IsNullOrEmpty(filter.Category))
                query = query.Where(lead => lead.Category.Contains(filter.Category));

            if (filter.DateCreatedStart.HasValue)
                query = query.Where(lead => lead.DateCreated >= filter.DateCreatedStart.Value);

            if (filter.DateCreatedEnd.HasValue)
                query = query.Where(lead => lead.DateCreated <= filter.DateCreatedEnd.Value);

            if (filter.Status.HasValue)
                query = query.Where(lead => lead.Status == filter.Status.Value);

            if (filter.MinPrice.HasValue)
                query = query.Where(lead => lead.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(lead => lead.Price <= filter.MaxPrice.Value);

            return query;
        }


        private bool IsValidLeadStatus(LeadStatusEnum status)
        {            
            return Enum.IsDefined(typeof(LeadStatusEnum), status);
        }

        private void HasDiscount(LeadModel lead, LeadStatusEnum status)
        {
            const decimal DiscountThreshold = 500;
            const decimal DiscountPercentage = 0.10m;
            
            if (status == LeadStatusEnum.Accepted && lead.Price > DiscountThreshold)
            {
                lead.Price -= lead.Price * DiscountPercentage;
            }
        }

   
    }
}

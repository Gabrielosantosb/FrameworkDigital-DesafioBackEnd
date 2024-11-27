
using AutoMapper;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;

namespace FrameworkDigital_DesafioBackEnd.Mappings
{
    public class LeadMappingProfile : Profile
    {
        public LeadMappingProfile()
        {
            //AutoMapper para CreateLead
            CreateMap<LeadRequest, LeadModel>()
                .AfterMap((leadRequest, leadModel) =>
                {
                    leadModel.Status = ORM.Enum.LeadStatusEnum.Invited;
                    leadModel.DateCreated = DateTime.Now;
                });

            //AutoMapper para UpdateLead
            CreateMap<LeadRequest, LeadModel>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())  // Ignora a atualização do status
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore());  // Ignora a data de criação
        }
    }
}

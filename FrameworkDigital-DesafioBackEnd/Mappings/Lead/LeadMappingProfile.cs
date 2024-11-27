
using AutoMapper;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;

namespace FrameworkDigital_DesafioBackEnd.Mappings
{
    public class LeadMappingProfile : Profile
    {
        public LeadMappingProfile()
        {
            CreateMap<LeadRequest, LeadModel>()
                .AfterMap((leadRequest, leadModel) =>
                {
                    leadModel.Status = ORM.Enum.LeadStatusEnum.Invited;
                    leadModel.DateCreated = DateTime.Now;
                });
        }
    }
}

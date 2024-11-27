using FrameworkDigital_DesafioBackEnd.ORM.Enum;

namespace FrameworkDigital_DesafioBackEnd.ORM.Model.Lead
{
    public class GetLeadsFilterDTO
    {
        public string? ContactFirstName { get; set; }
        public string? ContactLastName { get; set; }
        public string? ContactEmail { get; set; }
        public string? Suburb { get; set; }
        public string? Category { get; set; }
        public DateTime? DateCreatedStart { get; set; }  
        public DateTime? DateCreatedEnd { get; set; }    
        public LeadStatusEnum? Status { get; set; }      
        public decimal? MinPrice { get; set; }           
        public decimal? MaxPrice { get; set; }
    }
}

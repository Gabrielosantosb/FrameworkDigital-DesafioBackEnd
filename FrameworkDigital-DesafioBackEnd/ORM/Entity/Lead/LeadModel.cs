namespace FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead
{
    public class LeadModel
    {
        public int LeadId { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; } 
        public string ContactEmail { get; set; }    
        public string ContactPhoneNumber { get; set; } 
        public DateTime DateCreated { get; set; }
        public string Suburb { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsInvitedStatus { get; set; } 

    }
}

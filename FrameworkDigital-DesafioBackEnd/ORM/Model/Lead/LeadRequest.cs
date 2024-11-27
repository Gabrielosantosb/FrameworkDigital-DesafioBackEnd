namespace FrameworkDigital_DesafioBackEnd.ORM.Model.Lead
{
    public class LeadRequest
    {
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; }

        public string Suburb { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //public bool IsAccepted { get; set; }
    }
}

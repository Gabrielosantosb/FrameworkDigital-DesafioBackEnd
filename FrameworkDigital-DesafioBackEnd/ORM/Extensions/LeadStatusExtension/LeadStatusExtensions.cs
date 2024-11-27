using FrameworkDigital_DesafioBackEnd.ORM.Enum;

namespace FrameworkDigital.DesafioBackEnd.Extensions
{
    public static class LeadStatusExtensions
    {
        public static string ToShortCode(this LeadStatusEnum status)
        {
            return status switch
            {
                LeadStatusEnum.Invited => "INV",
                LeadStatusEnum.Accepted => "ACPTD",
                LeadStatusEnum.Declined => "DLD",
                _ => throw new ArgumentOutOfRangeException(nameof(status), "Status inválido")
            };
        }
    }
}

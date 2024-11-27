using System; // Necessário para referenciar a classe Exception

namespace FrameworkDigital_DesafioBackEnd.EmailException
{
    public class EmailSendException : Exception
    {
        public EmailSendException(string message) : base(message)
        {
        }
    }
}

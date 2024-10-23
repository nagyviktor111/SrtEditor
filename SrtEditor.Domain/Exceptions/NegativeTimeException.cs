namespace SrtEditor.Domain.Exceptions
{
    public class NegativeTimeException : Exception
    {
        public NegativeTimeException() : base()
        {
        }

        public NegativeTimeException(string message) : base(message)
        {
        }

        public NegativeTimeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

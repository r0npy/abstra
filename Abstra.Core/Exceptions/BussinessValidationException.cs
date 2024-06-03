namespace Abstra.Core.Exceptions
{
    public class BussinessValidationException : Exception
    {
        public BussinessValidationException() : base() { }

        public BussinessValidationException(string message) : base(message) { }

        public BussinessValidationException(string message, Exception innerException) : base(message, innerException) { }

    }
}

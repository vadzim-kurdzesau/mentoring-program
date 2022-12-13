using System;

namespace AdoNetFundamentals.Exceptions
{
    public class EntryDoesNotExistException : Exception
    {
        public EntryDoesNotExistException() 
        {
        }

        public EntryDoesNotExistException(string message)
            : base(message)
        {
        }

        public EntryDoesNotExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

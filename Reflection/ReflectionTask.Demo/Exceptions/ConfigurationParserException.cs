using System;

namespace ReflectionTask.Demo.Exceptions
{
    public class ConfigurationParserException : Exception
    {
        public ConfigurationParserException()
            : base()
        {
        }

        public ConfigurationParserException(string message)
            : base(message)
        {
        }

        public ConfigurationParserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

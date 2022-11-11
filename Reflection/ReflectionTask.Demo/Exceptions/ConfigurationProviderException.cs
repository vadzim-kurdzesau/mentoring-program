using System;

namespace ReflectionTask.Demo.Exceptions
{
    public class ConfigurationProviderException : Exception
    {
        public ConfigurationProviderException()
            : base()
        {
        }

        public ConfigurationProviderException(string message)
            : base(message)
        {
        }

        public ConfigurationProviderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

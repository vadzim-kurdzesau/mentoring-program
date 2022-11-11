using System;
using ReflectionTask.Demo.Exceptions;

namespace ReflectionTask.Demo.Converters
{
    public class ConfigurationParser : ITypeParser
    {
        /// <inheritdoc/>
        /// <exception cref="ConfigurationProviderException">Thrown, if <paramref name="type"/> is not supported.</exception>
        public dynamic Parse(Type type, string value)
        {
            try
            {
                if (type == typeof(string))
                {
                    return value;
                }

                if (type == typeof(int))
                {
                    return int.Parse(value);
                }

                if (type == typeof(float))
                {
                    return float.Parse(value);
                }

                if (type == typeof(TimeSpan))
                {
                    return TimeSpan.Parse(value);
                }
            }
            catch (FormatException ex)
            {
                throw new ConfigurationParserException(ex.Message);
            }

            throw new ConfigurationProviderException($"Configuration doesn't support the '{type.Name}' type.");
        }
    }
}

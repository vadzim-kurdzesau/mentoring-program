using System;
using ReflectionTask.Demo.Exceptions;

namespace ReflectionTask.Demo.Converters
{
    /// <summary>
    /// Parses values to specified types.
    /// </summary>
    public interface ITypeParser
    {
        /// <summary>
        /// Parses the <paramref name="value"/> to <paramref name="type"/>.
        /// </summary>
        /// <param name="type">Type of the parsed <paramref name="value"/>.</param>
        /// <param name="value">String to parse.</param>
        /// <exception cref="ConfigurationParserException">Thrown, if <paramref name="value"/> has invalid format.</exception>
        public dynamic Parse(Type type, string value);
    }
}

using System;
using DocumentLibrary.Models;

namespace DocumentLibrary.Service.Caching
{
    /// <summary>
    /// Contains cache options for a single <see cref="Document"/> type.
    /// </summary>
    public class DocumentCachingOptions
    {
        /// <summary>
        /// Gets or sets the lifetime of the <see cref="Document"/>.
        /// If null, <see cref="Document"/> will not be cached at all.
        /// </summary>
        public TimeSpan? ExpirationTime { get; set; }

        /// <summary>
        /// Gets a value indicating whether <see cref="Document"/> of this type should be cached.
        /// </summary>
        public bool ShouldBeCached => ExpirationTime.HasValue;
    }
}

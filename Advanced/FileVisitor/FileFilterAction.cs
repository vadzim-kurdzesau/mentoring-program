namespace FileVisitor
{
    /// <summary>
    /// File iteration action.
    /// </summary>
    public enum FileFilterAction
    {
        /// <summary>
        /// Specifies that file or directory should be included to final results and iteration completes.
        /// </summary>
        Include = 0,

        /// <summary>
        /// Specifies that file or directory should be excluded from final results and iteration completes.
        /// </summary>
        Exclude,

        /// <summary>
        /// Specifies that file iteration should be aborted.
        /// </summary>
        Abort
    }
}

using System;

namespace SpartanExtensions.UploadedFiles
{
    /// <summary>
    /// Uploaded file exception class
    /// </summary>
    public class UploadedFileException : Exception
    {
        internal UploadedFileException(string message)
            : base(message)
        {

        }
    }
}

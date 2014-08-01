using System;

namespace SpartanExtensions.UploadedFiles
{
    public class UploadedFileException : Exception
    {
        public UploadedFileException(string message)
            : base(message)
        {

        }
    }
}

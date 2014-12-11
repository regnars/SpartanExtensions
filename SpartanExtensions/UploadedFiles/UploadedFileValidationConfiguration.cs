using System.Collections.Generic;
namespace SpartanExtensions.UploadedFiles
{
    /// <summary>
    /// Configuration class for the uploaded files.
    /// </summary>
    public class UploadedFileValidationConfiguration
    {
        /// <summary>
        /// List of allowed uploadable file type extensions.
        /// </summary>
        public List<string> AllowedFileTypeExtensions { get; private set; }
        
        /// <summary>
        /// Maximum allowed uploaded file size in mega bytes.
        /// </summary>
        public int MaximumAllowedFileSizeInMb { get; private set; }
        
        /// <summary>
        /// Maximum allowed uploaded file size in bytes.
        /// </summary>
        public long MaximumAllowedFileSizeInBytes { get; private set; }

        /// <summary>
        /// Constructor for uploaded file configuration object.
        /// </summary>
        /// <param name="allowedFileTypeExtensions">List of allowed uploaded file type extensions without "." e.g. "txt,pdf,docx,xlsx".</param>
        /// <param name="maximumAllowedFileSizeInMb">Maximum allowed uploaded file size in mega bytes. e.g. 10</param>
        public UploadedFileValidationConfiguration(List<string> allowedFileTypeExtensions, int maximumAllowedFileSizeInMb)
        {
            AllowedFileTypeExtensions = allowedFileTypeExtensions;
            this.GuardAgainstNull(ufvc => ufvc.AllowedFileTypeExtensions);
            this.GuardAgainstEmptyEnumerable<UploadedFileValidationConfiguration, List<string>, string>(ftv => ftv.AllowedFileTypeExtensions);

            MaximumAllowedFileSizeInMb = maximumAllowedFileSizeInMb;
            this.GuardAgainstNegativeAndZero(ufvc => ufvc.MaximumAllowedFileSizeInMb);

            MaximumAllowedFileSizeInBytes = MaximumAllowedFileSizeInMb * 1000000;
        }
    }
}

using System.Collections.Generic;
namespace SpartanExtensions.UploadedFiles
{
    public class UploadedFileValidationConfiguration
    {
        public List<string> AllowedFileTypeExtensions { get; private set; }
        public int MaximumAllowedFileSizeInMb { get; private set; }
        public long MaximumAllowedFileSizeInBytes { get; private set; }

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

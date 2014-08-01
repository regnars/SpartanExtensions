using System.IO;

namespace SpartanExtensions.UploadedFiles
{
    public class UploadedFileValidator
    {
        private FileInfo FileInfo { get; set; }
        public string FileExtension { get; private set; }
        public string FileNameWithoutExtension { get; private set; }
        public string FileNameWithExtension { get; private set; }
        public string FilePath { get; private set; }
        public UploadedFileValidationConfiguration ValidationConfiguration { get; private set; }

        public UploadedFileValidator(FileInfo fileInfo, UploadedFileValidationConfiguration validationConfiguration)
        {
            FilePath = fileInfo.FullName;

            FileNameWithExtension = Path.GetFileName(FilePath);
            this.GuardAgainstStringIsNullOrEmpty(ufv => ufv.FilePath);

            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FilePath);
            this.GuardAgainstStringIsNullOrEmpty(ufv => ufv.FileNameWithoutExtension);

            FileExtension = Path.GetExtension(FilePath);
            this.GuardAgainstStringIsNullOrEmpty(ufv => ufv.FileExtension);

            ValidationConfiguration = validationConfiguration;
            this.GuardAgainstNull(ufv => ufv.ValidationConfiguration);
        }

        public void ValidateAgainstAllowedExtensions()
        {
            if (!ValidationConfiguration.AllowedFileTypeExtensions.Contains(FileExtension))
                throw new UploadedFileException(string.Format("\"{0}\" is not an allowed file extension - \"{1}\"",
                    FileExtension, string.Join(",", ValidationConfiguration.AllowedFileTypeExtensions)));
        }

        public void ValidateFileSize()
        {
            if (ValidationConfiguration.MaximumAllowedFileSizeInBytes < FileInfo.Length)
                throw new UploadedFileException(
                    string.Format("File size exceeds the allowed maximum of {0} mb.", ValidationConfiguration.MaximumAllowedFileSizeInMb));

        }
    }
}

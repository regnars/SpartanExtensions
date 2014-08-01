using System;
using Microsoft.Win32;
using System.IO;

namespace SpartanExtensions.UploadedFiles
{
    public class UploadedFileValidator
    {
        public string FileMimeType { get; private set; }
        public string FileExtensionByMimeType { get; private set; }
        private FileInfo FileInfo { get; set; }
        public string FileExtensionByFileName { get; private set; }
        public string FileNameWithoutExtension { get; private set; }
        public string FileNameWithExtension { get; private set; }
        public string FilePath { get; private set; }
        public UploadedFileValidationConfiguration ValidationConfiguration { get; private set; }

        public UploadedFileValidator(FileInfo fileInfo, UploadedFileValidationConfiguration validationConfiguration)
        {
            FileInfo = fileInfo;
            this.GuardAgainstNull(ufv => ufv.FileInfo);

            FilePath = FileInfo.FullName;
            this.GuardAgainstStringIsNullOrEmpty(ufv => ufv.FilePath);

            FileNameWithExtension = Path.GetFileName(FilePath);
            this.GuardAgainstStringIsNullOrEmpty(ufv => ufv.FilePath);

            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FilePath);
            this.GuardAgainstStringIsNullOrEmpty(ufv => ufv.FileNameWithoutExtension);

            FileExtensionByFileName = Path.GetExtension(FilePath).Replace(".", string.Empty);
            this.GuardAgainstStringIsNullOrEmpty(ufv => ufv.FileExtensionByFileName);

            FileMimeType = GetMimeType();
            this.GuardAgainstStringIsNullOrEmpty(ufv => ufv.FileMimeType);

            FileExtensionByMimeType = GetExtensionByMimeType(FileMimeType);

            ValidationConfiguration = validationConfiguration;
            this.GuardAgainstNull(ufv => ufv.ValidationConfiguration);
        }

        private string GetExtensionByMimeType(string mimeType)
        {
            var key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false);
            var value = key != null ? key.GetValue("Extension", null) : null;
            return value != null ? value.ToString().Replace(".", string.Empty) : string.Empty;
        }

        private string GetMimeType()
        {
            return File.ReadAllBytes(FilePath).GetMimeType();
        }

        public void ValidateExtension()
        {
            ValidateAgainstAllowedExtensions();
            ValidateSpecifiedExtensionInFileNameAgainstActualExtension();
        }

        public void ValidateAgainstAllowedExtensions()
        {
            if (!ValidationConfiguration.AllowedFileTypeExtensions.Contains(FileExtensionByFileName))
                throw new UploadedFileException(string.Format("\"{0}\" is not an allowed file extension - \"{1}\"",
                    FileExtensionByFileName, string.Join(",", ValidationConfiguration.AllowedFileTypeExtensions)));
        }

        public void ValidateSpecifiedExtensionInFileNameAgainstActualExtension()
        {
            if (String.IsNullOrEmpty(FileExtensionByMimeType))
                throw new UploadedFileException(string.Format("File's \"{0}\" type is not recognised.", FileNameWithExtension));

            if (!String.IsNullOrEmpty(FileExtensionByMimeType)
                && FileExtensionByFileName != FileExtensionByMimeType)
                throw new UploadedFileException(string.Format("File extension \"{0}\" specified in file name \"{1}\" does not comply with the actual file extension \"{2}\".",
                    FileExtensionByFileName, FileNameWithExtension, FileExtensionByMimeType));
        }

        public void ValidateFileSize()
        {
            if (ValidationConfiguration.MaximumAllowedFileSizeInBytes < FileInfo.Length)
                throw new UploadedFileException(
                    string.Format("File size exceeds the allowed maximum of {0} mb.", ValidationConfiguration.MaximumAllowedFileSizeInMb));

        }
    }
}

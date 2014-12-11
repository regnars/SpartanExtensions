using System;
using System.Linq;
using Microsoft.Win32;
using System.IO;

namespace SpartanExtensions.UploadedFiles
{
    /// <summary>
    /// Uploaded file validator class.
    /// </summary>
    public class UploadedFileValidator
    {
        /// <summary>
        /// File mime type.
        /// </summary>
        public string FileMimeType { get; private set; }
        
        /// <summary>
        /// File extension determined by mime type.
        /// </summary>
        public string FileExtensionByMimeType { get; private set; }
        
        /// <summary>
        /// FileInfo type object representation of the file to validate.
        /// </summary>
        private FileInfo FileInfo { get; set; }

        /// <summary>
        /// File extension determined by file name.
        /// </summary>
        public string FileExtensionByFileName { get; private set; }
        
        /// <summary>
        /// File name without an extension.
        /// </summary>
        public string FileNameWithoutExtension { get; private set; }
        
        /// <summary>
        /// File name with an extension.
        /// </summary>
        public string FileNameWithExtension { get; private set; }
        
        /// <summary>
        /// File path.
        /// </summary>
        public string FilePath { get; private set; }
        
        /// <summary>
        /// Validation configuration object.
        /// </summary>
        public UploadedFileValidationConfiguration ValidationConfiguration { get; private set; }

        private OpenXmlFormats OpenXmlFormats { get; set; }


        /// <summary>
        /// Constructor for uploaded file validator object.
        /// </summary>
        /// <param name="fileInfo">FileInfo type object representation of the file to validate.</param>
        /// <param name="validationConfiguration">Uploaded file validation configuration</param>
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

            OpenXmlFormats = new OpenXmlFormats();
        }

        private static string GetExtensionByMimeType(string mimeType)
        {
            var key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false);
            var value = key != null ? key.GetValue("Extension", null) : null;
            return value != null ? value.ToString().Replace(".", string.Empty) : string.Empty;
        }

        private string GetMimeType()
        {
            return File.ReadAllBytes(FilePath).GetMimeType();
        }

        /// <summary>
        /// Validates file's extension - performs validation against allowed extensions and checks if the specified file extension is the actual extension of the file.
        /// For example, if the .exe file is simply renamed to .txt extension and uploaded to the server, then the validator will throw an exception that the file is invalid.
        /// </summary>
        public void ValidateExtension()
        {
            ValidateAgainstAllowedExtensions();
            ValidateSpecifiedExtensionInFileNameAgainstActualExtension();
        }

        /// <summary>
        /// Validates file's extension - performs only validation against allowed extensions.
        /// </summary>
        public void ValidateAgainstAllowedExtensions()
        {
            if (!ValidationConfiguration.AllowedFileTypeExtensions.Contains(FileExtensionByFileName))
                throw new UploadedFileException(string.Format("\"{0}\" is not an allowed file extension - \"{1}\"",
                    FileExtensionByFileName, string.Join(",", ValidationConfiguration.AllowedFileTypeExtensions)));
        }

        /// <summary>
        /// Validates file's extension - checks if the specified file extension is the actual extension of the file.
        /// For example, if the .exe file is simply renamed to .txt extension and uploaded to the server, then the validator will throw an exception that the file is invalid.
        /// </summary>
        public void ValidateSpecifiedExtensionInFileNameAgainstActualExtension()
        {
            if (String.IsNullOrEmpty(FileExtensionByMimeType))
                throw new UploadedFileException(string.Format("File's \"{0}\" type is not recognised.", FileNameWithExtension));

            if (!String.IsNullOrEmpty(FileExtensionByMimeType)
                && FileExtensionByFileName != FileExtensionByMimeType)
            {
                if (OpenXmlFormats.Any(oxf => oxf.Formats.Any(f => f.ContainsValue(FileExtensionByFileName)))
                    && FileExtensionByMimeType == "zip")
                    return;

                throw new UploadedFileException(string.Format("File extension \"{0}\" specified in file name \"{1}\" does not comply with the actual file extension \"{2}\".",
                    FileExtensionByFileName, FileNameWithExtension, FileExtensionByMimeType));
            }
        }

        /// <summary>
        /// Validates uploaded file size.
        /// </summary>
        public void ValidateFileSize()
        {
            if (ValidationConfiguration.MaximumAllowedFileSizeInBytes < FileInfo.Length)
                throw new UploadedFileException(
                    string.Format("File size exceeds the allowed maximum of {0} mb.", ValidationConfiguration.MaximumAllowedFileSizeInMb));

        }
    }
}

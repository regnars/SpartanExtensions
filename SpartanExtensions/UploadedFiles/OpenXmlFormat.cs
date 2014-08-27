using System.Collections.Generic;

namespace SpartanExtensions.UploadedFiles
{
    internal class OpenXmlFormat
    {
        public string Application { get; private set; }
        public List<Dictionary<string, string>> Formats { get; private set; }

        public OpenXmlFormat(string application, List<Dictionary<string, string>> formats)
        {
            Application = application;
            this.GuardAgainstStringIsNullOrEmpty(oxf => oxf.Application);

            Formats = formats;
            this.GuardAgainstNull(oxf => oxf.Formats);
            this.GuardAgainstEmptyEnumerable<OpenXmlFormat,
                List<Dictionary<string, string>>, Dictionary<string, string>>(oxf => oxf.Formats);
        }
    }
}

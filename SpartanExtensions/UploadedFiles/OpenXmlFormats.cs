using System.Collections.Generic;

namespace SpartanExtensions.UploadedFiles
{
    internal class OpenXmlFormats : List<OpenXmlFormat>
    {
        public OpenXmlFormats()
        {
            Add(new OpenXmlFormat(application: "Word",
                formats: new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        { "Document", "docx" },
                        { "Macro-enabled document", "docm" },
                        { "Macro-enabled template", "dotm"}
                    }
                }));

            Add(new OpenXmlFormat(application: "Excel",
                formats: new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        { "Workbook", "xlsx" },
                        { "Macro-enabled workbook", "xlsm" },
                        { "Template", "xltx" },
                        { "Macro-enabled template", "xltm" },
                        { "Non-XML binary workbook", "xlsb" },
                        { "Macro-enabled add-in", "xlam" }
                    }
                }));

            Add(new OpenXmlFormat(application: "PowerPoint",
                formats: new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        {"Presentation", "pptx"},
                        {"Macro-enabled presentation", "pptm"},
                        {"Template", "potx"},
                        {"Macro-enabled template", "potm"},
                        {"Macro-enabled add-in", "ppam"},
                        {"Show", "ppsx"},
                        {"Macro-enabled show", "ppsm"},
                        {"Slide", "sldx"},
                        {"Macro-enabled slide", "sldm"},
                        {"Office theme", "thmx"}   
                    }
                }));
        }
    }
}

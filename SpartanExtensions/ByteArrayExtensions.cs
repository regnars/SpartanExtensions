using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SpartanExtensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Writes byte array content to FileStream object.
        /// </summary>
        public static FileInfo ToFile(this byte[] content, string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            fileStream.Write(content, 0, content.Length);
            fileStream.Close();
            return new FileInfo(filePath);
        }

        /// <summary>
        /// Gets mime type of the byte array (file content).
        /// </summary>
        public static string GetMimeType(this byte[] content)
        {
            const string defaultMimeType = "application/octet-stream";
            try
            {
                uint mimeType;
                FindMimeFromData(0, null, content, 256, null, 0, out mimeType, 0);

                var mimePointer = new IntPtr(mimeType);
                var mime = Marshal.PtrToStringUni(mimePointer);
                Marshal.FreeCoTaskMem(mimePointer);

                return mime ?? defaultMimeType;
            }
            catch
            {
                return defaultMimeType;
            }
        }

        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static uint FindMimeFromData(
            uint pBc,
            [MarshalAs(UnmanagedType.LPStr)] string pwzUrl,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
            uint cbSize,
            [MarshalAs(UnmanagedType.LPStr)] string pwzMimeProposed,
            uint dwMimeFlags,
            out uint ppwzMimeOut,
            uint dwReserverd
        );
    }
}

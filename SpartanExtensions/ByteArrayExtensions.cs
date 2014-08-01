using System.IO;
namespace SpartanExtensions
{
    public static class ByteArrayExtensions
    {
        public static FileInfo ToFile(this byte[] content, string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            fileStream.Write(content, 0, content.Length);
            fileStream.Close();
            return new FileInfo(filePath);
        }
    }
}

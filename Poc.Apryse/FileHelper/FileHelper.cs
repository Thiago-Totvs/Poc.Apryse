namespace AprysePoc.FileHelper
{
    public static class FileHelper
    {
        public static byte[] GetBytesFromFormFile(this IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}

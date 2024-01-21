namespace Indigo.Helpers
{
    public static class RemoveFile
    {
        public static void DeleteFile(this string filePath)
        {
            var targetFile = Path.Combine(PathConstants.RootPath, PathConstants.ImagesFileLocation, filePath);

            if (System.IO.File.Exists(targetFile)) System.IO.File.Delete(targetFile);
        }
    }
}

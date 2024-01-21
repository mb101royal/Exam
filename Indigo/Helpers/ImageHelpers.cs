namespace Indigo.Helpers
{
    public static class ImageHelpers
    {
        public static bool IsCorrectType(this IFormFile imageFile)
            => imageFile.ContentType.Contains("image");

        public static bool IsCorrectSize(this IFormFile imageFile, float kb = 200)
            => imageFile.Length <= kb * 1024;

        public static async Task<string> SaveImageAsync(this IFormFile imageFile, string ImageToSavePath)
        {
            string imageFileName = Guid.NewGuid() + imageFile.FileName;
            string imageFilePath = Path.Combine(PathConstants.RootPath, ImageToSavePath, imageFileName);

            if (!Directory.Exists(Path.Combine(PathConstants.RootPath, ImageToSavePath)))
            {
                Directory.CreateDirectory(Path.Combine(PathConstants.RootPath, ImageToSavePath));
            }

            using (FileStream fs = new(imageFilePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fs);
            }

            return imageFileName;
        } 
    }
}

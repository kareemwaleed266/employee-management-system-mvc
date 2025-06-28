namespace Demo.PL.Helper
{
    public static class DocumentHelper
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            // 1.Get Located Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files",folderName);
           
            // 2.Get file Name & Make it Unique
            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";

            //3.Get file Path
            var filePath= Path.Combine(folderPath,fileName);

            /////////////////////////////////////////////////////
            
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }
    }
}

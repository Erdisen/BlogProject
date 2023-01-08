using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BlogProject.WebUI.Areas.Administrator.Models
{
    public class Upload
    {
        public static string ImageUpload(List<IFormFile> files,IHostingEnvironment _env, out bool result)
        {
            //REsim yükleme işlemlerimizi gerçekleştireceğiz gereye reim yolunu veya hata mesajını döndüreceğiz. Ayrıca dönen string'in başarı bilgisi mi yoksa hata mesajımı olduğunu anlamak için dışarı result değeri fırlatacağız.
            result = false;
            var uploads = Path.Combine(_env.WebRootPath, "Uploads");
            foreach (var file in files)
            {
                if (file.ContentType.Contains("image")) // Dosya tipinde image geçiyorsa
                {
                    if (file.Length <= 1048576) // Dosya boyutu 2 MB' ten küçük ise
                    {
                        string uniqueName = $"{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{file.ContentType.Split('/')[1]}";
                        var filePath = Path.Combine(uploads, uniqueName);

                    }
                }
            }
        }
    }
}

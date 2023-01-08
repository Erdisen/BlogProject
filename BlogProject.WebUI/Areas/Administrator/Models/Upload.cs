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
                    //1 MB = 1024 KB 
                    //1 KB = 1024 B
                    if (file.Length <= 2097152) // Dosya boyutu 2 MB' ten küçük ise
                    {
                        string uniqueName = $"{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{file.ContentType.Split('/')[1]}";
                        var filePath = Path.Combine(uploads, uniqueName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            result = true;
                            return filePath.Substring(filePath.IndexOf("\\Uploads\\"));
                        }
                    }
                    else
                    {
                        return $"2 MB'ten büyük boyutta resim yükleyemezsiniz.";
                    }
                }
                else
                {
                    return $"Lütfen sadece resim dosyası yükleyin";
                }
            }
            return "Dosya Bulunamadı! Lütfen en az bir tane dosya seçin";
        }
    }
}

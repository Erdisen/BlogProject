using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class CategoryController : Controller
    {
        private readonly ICoreService<Category> _categoryService;
        public CategoryController(ICoreService<Category> categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View(_categoryService.GetAll());
        }
        [HttpGet] //Create sayfasını gösterecek
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] //Create sayfasından gelen veriyi Db ye ekleyecek
        public IActionResult Create(Category category)
        {
            category.Status = Core.Entity.Enum.Status.None;
            #region ModelState Kontrol
            if (ModelState.IsValid)
            {
                bool result = _categoryService.Add(category);
                if (result)
                {
                    TempData["Message"] = $"Kayıt işlemi başarılı!";
                    return RedirectToAction("Index"); // ekleme işlemi başarılı ise Index'e döndürebiliriz.
                }
                else
                {

                    TempData["Message"] = $"Kayıt işlemi sırasında bir hata meydana geldi. Lütfen tüm alanları kontrol edip tekrar deneyin!";
                   
                }

            }
            else
            {
                TempData["Message"] = $"İşlem başarısız oldu. Lütfen tüm alanları kontrol edip tekrar deneyin! ";
            }
            #endregion
            return View(category); // Ekleme işlemi sırasında kullanılan category bilgileriyle View'a döndürmesini sağlayabilir.
        }
        [HttpGet] //Create sayfasını gösterecek
        public IActionResult Update(Guid id)
        {
            
            return View(_categoryService.GetByID(id));
        }
        [HttpPost] //update sayfasından gelen veriyi DB'de güncelleyecek
        public IActionResult Update(Category category)
        {
          
          
            if (ModelState.IsValid)
            {
                Category updateCategory = _categoryService.GetByID(category.ID);
                updateCategory.Description = category.Description;
                updateCategory.CategoryName = category.CategoryName;
                updateCategory.Status = Core.Entity.Enum.Status.Update;

                bool result = _categoryService.Update(updateCategory);
                if (result)
                {
                    TempData["Message"] = $"Kayıt işlemi başarılı!";
                    return RedirectToAction("Index"); // ekleme işlemi başarılı ise Index'e döndürebiliriz.
                }
                else
                {

                    TempData["Message"] = $"Kayıt işlemi sırasında bir hata meydana geldi. Lütfen tüm alanları kontrol edip tekrar deneyin!";

                }

            }
            else
            {
                TempData["Message"] = $"İşlem başarısız oldu. Lütfen tüm alanları kontrol edip tekrar deneyin! ";
            }
            return View(category); // Ekleme işlemi sırasında kullanılan category bilgileriyle View'a döndürmesini sağlayabilir.
            
        }
        public IActionResult Activate(Guid id) //gelen id'ye göre ilgili nesneyi güncelleyecek
        {
            //view göstermeyeccek Index'e yönlendirebiliriz
            _categoryService.Activate(id);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(Guid id) //Gleen Id'ye göre ilgili nesneyi silecek
        {
            _categoryService.Remove(_categoryService.GetByID(id));
            return RedirectToAction("Index");
        }
    }
}

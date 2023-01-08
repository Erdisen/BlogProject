﻿using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class PostController : Controller
    {
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<Category> _categoryService;

        public PostController(ICoreService<Post> postService, ICoreService<Category> categoryService)
        {
            _postService = postService;
            this._categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View(_postService.GetAll());
        }
        [HttpGet] //Create sayfasını gösterecek
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetActive(),"ID","CategoryName");
            return View();
        }
        [HttpPost] //Create sayfasından gelen veriyi Db ye ekleyecek
        public IActionResult Create(Post post)
        {
            post.Status = Core.Entity.Enum.Status.None;
            #region ModelState Kontrol
            if (ModelState.IsValid)
            {
                bool result = _postService.Add(post);
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
            return View(post); // Ekleme işlemi sırasında kullanılan category bilgileriyle View'a döndürmesini sağlayabilir.
        }
        [HttpGet] //Create sayfasını gösterecek
        public IActionResult Update(Guid id)
        {

            return View(_postService.GetByID(id));
        }
        [HttpPost] //update sayfasından gelen veriyi DB'de güncelleyecek
        public IActionResult Update(Post post)
        {


            if (ModelState.IsValid)
            {
                Post updatePost = _postService.GetByID(post.ID);
                updatePost.Title = post.Title ;
                updatePost.PostDetail = post.PostDetail;
                updatePost.ImagePath = post.ImagePath;
                updatePost.Tags = post.Tags;
                updatePost.CategoryID = post.CategoryID;

                bool result = _postService.Update(updatePost);
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
            return View(post); // Ekleme işlemi sırasında kullanılan category bilgileriyle View'a döndürmesini sağlayabilir.

        }
        public IActionResult Activate(Guid id) //gelen id'ye göre ilgili nesneyi güncelleyecek
        {
            //view göstermeyeccek Index'e yönlendirebiliriz
            _postService.Activate(id);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(Guid id) //Geleen Id'ye göre ilgili nesneyi silecek
        {
            _postService.Remove(_postService.GetByID(id));
            return RedirectToAction("Index");
        }
    }
}

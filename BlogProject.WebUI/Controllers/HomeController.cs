using BlogProject.Core.Entity.Enum;
using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using BlogProject.WebUI.Models;
using BlogProject.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICoreService<Category> _catService;
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<User> _userService;
        private readonly ICoreService<Comment> _commentService;

        public HomeController(ILogger<HomeController> logger, ICoreService<Category> catService, ICoreService<Post> postService, ICoreService<User> userService,ICoreService<Comment> commentService)
        {
            _logger = logger;
            _catService = catService;
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
        }
        public IActionResult Index()
        {
            //Aktif olan postları döndürelim.
            return View(_postService.GetActive());
        }

        public IActionResult PostsByCatID(Guid id) // Kategori ID'si
        {
            //Kategori ID'ye göre aktif postları döndürelim.
            return View(_postService.GetDefault(x=>x.CategoryID==id));
        }
        public IActionResult Post(Guid id) // Gönderinin ID'si
        {
            //Post'u göstereceğiz. Gösterirken de, okunma sayısını (ViewCount) 1 artıralım.
            Post okunanPost = _postService.GetByID(id);
            okunanPost.ViewCount++;
            _postService.Update(okunanPost);

            PostDetailVM vm = new PostDetailVM();
            vm.Post = okunanPost;
            vm.Category = _catService.GetByID(okunanPost.CategoryID);
            vm.User = _userService.GetByID(okunanPost.UserID);
            vm.Comments = _commentService.GetDefault(x => x.Status == Status.Active && x.PostID == okunanPost.ID);

            return View(vm);  //View'a döndürürken ilgili postu, kategorisini, yazarını(kullanıcıyı) döndürmemiz gerekecektir (birden fazla model). Bu sebeple Tuple ya da ViewModel yapısını kullanmalıyız.
        }




    }
}
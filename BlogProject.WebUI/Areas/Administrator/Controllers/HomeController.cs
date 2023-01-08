using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using Microsoft.AspNetCore.Mvc;


namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class HomeController : Controller
    {        
        private readonly ICoreService<Category> _catService;
        private readonly ICoreService<User> _userService;
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<Comment> _commentService;

        public HomeController(ICoreService<Category> catService, ICoreService<Post> postService, ICoreService<User> userService, ICoreService<Comment> commentService)
        {            
            _catService = catService;
            _userService = userService;
            _postService = postService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            ViewBag.Kullanici = _userService.GetActive().Count;
            ViewBag.Kategori = _catService.GetActive().Count;
            ViewBag.Post = _postService.GetActive().Count;
            ViewBag.Yorum = _commentService.GetActive().Count;

            return View(_postService.GetActive());
        }
    }
}

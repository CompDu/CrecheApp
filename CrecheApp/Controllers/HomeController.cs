using CrecheApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using DataLibrary.BusinessLogic;


namespace CrecheApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IForumAccess forumData;
        
        public HomeController(ILogger<HomeController> logger, IForumAccess _forumAccess)
        {
            _logger = logger;
            forumData = _forumAccess;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reply()
        {
            return View();
        }

        public IActionResult CreateComment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateComment(Models.ForumModel ForumComment)
        {
            ForumComment.UserId = User.Identity.Name;
            //ForumComment.CommentId = "1";

            if (ModelState.IsValid) {
                DataLibrary.ForumModel param = new DataLibrary.ForumModel{ Comment = ForumComment.Comment, 
                                                                           UserId = ForumComment.UserId};
                forumData.SetData(param);
            }
            return RedirectToAction("Forum");
        }

        [Authorize]
        public IActionResult Forum()
        {
            ViewData["Message"] = "Forum Page";


            var data = forumData.GetData<DataLibrary.ForumModel>();
            List<Models.ForumModel> CommentsList = new List<Models.ForumModel>();

            foreach(var row in data) {
                Models.ForumModel Comment = new Models.ForumModel();

                Comment.CommentId = row.CommentId;
                Comment.UserId = row.UserId;
                Comment.CreatedDate = row.CreatedDate;
                Comment.Comment = row.Comment;

                CommentsList.Add(Comment);
            }

            return View(CommentsList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
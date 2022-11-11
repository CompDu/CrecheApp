using CrecheApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using DataLibrary.BusinessLogic;
using Dapper;

namespace CrecheApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ForumAccess forumData;
        
        public HomeController(ILogger<HomeController> logger, ForumAccess _forumAccess)
        {
            _logger = logger;
            forumData = _forumAccess;
        }
       

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet] 
        public IActionResult Edit(int id)
        {

           // var arguments = new DynamicParameters();
           // arguments.Add("@CommentId",id);
            
          //  List<DataLibrary.ForumModel> Comments = forumData.GetData<DataLibrary.ForumModel>("dbo.spForum_GetRecord",arguments);
            DataLibrary.ForumModel DforumModel = forumData.GetRow(id);//Comments.FirstOrDefault();
            CrecheApp.Models.ForumModel forumModel = new ForumModel() {
                CommentId = DforumModel.CommentId,
                CreatedDate = DforumModel.CreatedDate,
                Comment = DforumModel.Comment,
                UserId = DforumModel.UserId

            };
            if(forumModel != null && forumModel.UserId.Equals(User.Identity.Name)) {
                return View(forumModel);
            }
            else {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(ForumModel forumModel)
        {
            DataLibrary.ForumModel MyModel = new DataLibrary.ForumModel() {
                CommentId=forumModel.CommentId,
                Comment = forumModel.Comment,
                UserId = User.Identity.Name
            };
            
            //forumData.SetData("dbo.UpdateComment", arguments);
            forumData.Update(MyModel);//need to do an upsert
            
            return RedirectToAction("Forum");
        
        }

        public IActionResult Reply()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateComment(Models.ForumModel forumModel)
        {

            DataLibrary.ForumModel MyModel = new DataLibrary.ForumModel() {
                CreatedDate = DateTime.Now,
                Comment = forumModel.Comment,
                UserId = User.Identity.Name
            };
          //  var arguments = new DynamicParameters();
          //  arguments.Add("@CommentId",MyModel.CommentId);
          //  arguments.Add("@Comment", MyModel.Comment);
          //  forumData.SetData("dbo.spForum_InsertComment",MyModel);
            forumData.Create(MyModel);

           return RedirectToAction("Forum");
        }
        public IActionResult CreateComment()
        {

            return View();
        }
       

        [Authorize]
        public IActionResult Forum()
        {
          ViewData["Message"] = "Forum Page";

          
          var data = forumData.GetRows();
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

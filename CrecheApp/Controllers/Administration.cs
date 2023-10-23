using CrecheApp.Models;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CrecheApp.Controllers
{

    [Route("{controller=Administration}/{action}/{id?}")]
    public class Administration : Controller
    {
        //-bar forum users
        //-moderate comments
        //-add other admn users
        //-approve registrations
        //-remove unsuccessful applications

        private ForumAccess forumdb;
        public Administration(ForumAccess _forumdb)
        {
            forumdb= _forumdb;
        }

        public IActionResult AdminIndex()
        {
            return View();
        }

        public IActionResult ModerateForum()
        {
            List<DataLibrary.ForumModel> ForumList = forumdb.GetRows();
            List<Models.ForumModel> forumList= new List<Models.ForumModel>();
            foreach(var item in ForumList) {
               Models.ForumModel forumModel = new Models.ForumModel {
                   CommentId = item.CommentId,
                   Comment = item.Comment,
                   CreatedDate= item.CreatedDate,
                   UserId = item.UserId,
               };
                forumList.Add(forumModel);
            }
            return View(forumList);
        }
    }
}

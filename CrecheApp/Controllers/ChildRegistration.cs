using CrecheApp.Models;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataLibrary.Models;
using ParentModel = CrecheApp.Models.ParentModel;
using ChildModel = DataLibrary.Models.ChildModel;

namespace CrecheApp.Controllers
{
    [Authorize]
    public class ChildRegistration : Controller
    {
        
        private ParentAccess parentDb;
        private ChildAccess childDb;    

        public ChildRegistration(ParentAccess _parentDb, ChildAccess _childDb)
        {
            parentDb = _parentDb;
            childDb = _childDb;
            
        }

        [HttpGet]
        public IActionResult ChildDetails(int ParentId)
        {
            List<ChildModel> children = childDb.GetRows(ParentId);
            List<Models.ChildModel> _children = new List<Models.ChildModel>();
            foreach (var child in children) {
                Models.ChildModel _child = new Models.ChildModel {
                    Id = child.Id,
                    ParentId = child.ParentId,
                    Name = child.Name,
                    Surname = child.Surname,
                    IDNumber = child.IDNumber
                };
                _children.Add(_child);
            }
            return View(_children);
        }
        [HttpPost]
        public IActionResult ChildDetails(Models.ChildModel childModel)
        {
            List<ChildModel> children = childDb.GetRows(childModel.ParentId);
            List<Models.ChildModel> _children = new List<Models.ChildModel>();
            foreach( var child in children) {
                Models.ChildModel _child = new Models.ChildModel {Id = child.Id,
                                                                  ParentId = child.ParentId,
                                                                  Name = child.Name,
                                                                  Surname = child.Surname,
                                                                  IDNumber = child.IDNumber
                                                                 };
                _children.Add(_child);
            }
            return View(_children);
        }

        [HttpPost]
        public IActionResult ParentDetails(ParentModel parentModel)
        {
            if (ModelState.IsValid) {
                
                DataLibrary.Models.ParentModel _parentModel = new DataLibrary.Models.ParentModel {
                    Id = parentModel.Id,
                    Name = parentModel.Name,
                    Surname = parentModel.Surname,
                    IDNumber = parentModel.IDNumber,
                    Address = parentModel.Address,
                    PhoneNumber = parentModel.PhoneNumber,
                    UserId = User.Identity.Name
                };
                parentDb.SetRows(_parentModel);
                return View(parentModel);
            }
            return RedirectToAction("Error");

        }

        
        [HttpGet]
        public IActionResult ParentDetails(int Id)
        {
            DataLibrary.Models.ParentModel model = parentDb.GetRow(Id);
            if (model != null) {
                ParentModel RegisteredParent = new ParentModel {
                    Id = model.Id,
                    Name = model.Name,
                    Surname = model.Surname,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    IDNumber = model.IDNumber,
                    UserId = model.UserId
                };
                return View(RegisteredParent);
            }
            else {
                return RedirectToAction("Error");
            }
            
            
        }
        [HttpGet]
        public IActionResult EditParent(int Id)
        {
            DataLibrary.Models.ParentModel model = parentDb.GetRow(Id);
            if (model != null) {
                ParentModel RegisteredParent = new ParentModel {
                    Id = model.Id,
                    Name = model.Name,
                    Surname = model.Surname,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    IDNumber = model.IDNumber,
                    UserId = model.UserId
                };
                return View(RegisteredParent);
            }
            else {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public IActionResult EditParent(ParentModel parentModel)
        {
            if(ModelState.IsValid) {

                DataLibrary.Models.ParentModel model = new DataLibrary.Models.ParentModel {
                    Id = parentModel.Id,
                    Name = parentModel.Name,
                    Surname = parentModel.Surname,
                    Address = parentModel.Address,
                    PhoneNumber = parentModel.PhoneNumber,
                    IDNumber = parentModel.IDNumber,
                    UserId = parentModel.UserId
                };

                parentDb.EditRow(model);
                return RedirectToAction("ParentDetails",model.Id);
            }
            return View("Error");
        }

        [HttpGet]
       public IActionResult CreateChild(int ParentId)
       {
            Models.ChildModel childModel = new Models.ChildModel() {    Id = 0,
                                                                        ParentId = ParentId};
            return View(childModel);  
       }
     
        [HttpPost]
       public IActionResult CreateChild(Models.ChildModel childModel)
       {
            if (ModelState.IsValid) {
                    DataLibrary.Models.ChildModel child = new ChildModel { Id = childModel.Id,
                    ParentId = childModel.ParentId,
                    Name = childModel.Name,
                    Surname = childModel.Surname,
                    IDNumber = childModel.IDNumber };

                childDb.SetRow(child);
                return RedirectToAction("ChildDetails",childModel);
            }

            return RedirectToAction("Error");
       }

        [HttpGet]
        public IActionResult Registration()
        {
            //do a search for registered parent
            //if found redirect to parent details
            DataLibrary.Models.ParentModel model = parentDb.GetRow(User.Identity.Name);
            
            if (model != null) {
                //Found a registered parent
               
                return RedirectToAction("ParentDetails",model.Id);
            }
            ParentModel parent = new ParentModel { Id = 1, 
                                                   UserId = User.Identity.Name};
                                                                
            return View(parent);    
        }
      
     /*   [HttpPost]
        public IActionResult Registration(ParentModel parentModel)
        {
            if (ModelState.IsValid) {
                return RedirectToAction("ParentDetails", parentModel);
            }
            return RedirectToAction("Error");
        }*/
    }
}

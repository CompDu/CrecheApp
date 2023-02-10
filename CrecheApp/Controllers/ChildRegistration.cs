using CrecheApp.Models;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataLibrary.Models;
using ParentModel = DataLibrary.Models.ParentModel;
using ChildModel = DataLibrary.Models.ChildModel;
using PaymentModel = DataLibrary.Models.PaymentModel;


namespace CrecheApp.Controllers
{
    [Authorize]
    [Route("{controller=Home}/{action=Index}/{Id?}")]
    public class ChildRegistration : Controller
    {

        private ParentAccess parentDb;
        private ChildAccess childDb;
        private PaymentAccess paymentDb;
        private const decimal PaymentPerChild = 300.00M;

        public ChildRegistration(ParentAccess _parentDb, ChildAccess _childDb, PaymentAccess _paymentDb)
        {
            parentDb = _parentDb;
            childDb = _childDb;
            paymentDb = _paymentDb;
        }

        public IActionResult PaymentDetails(int Id)
        {
            List<PaymentModel> PaymentsList = paymentDb.GetRows(Id);
            List<Models.PaymentModel> _PaymentsList = new List<Models.PaymentModel>();
            foreach (var payment in PaymentsList) {
                Models.PaymentModel _payment = new Models.PaymentModel {
                    Id = payment.Id,
                    ParentId = payment.ParentId,
                    Amount = payment.Amount,
                    Ref = payment.Ref
                };
                _PaymentsList.Add(_payment);
            }
            return View(_PaymentsList);
        }

        [HttpGet]

        public IActionResult ChildDetails(int Id)
        {
            List<ChildModel> children = childDb.GetRows(Id);
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
            foreach (var child in children) {
                Models.ChildModel _child = new Models.ChildModel { Id = child.Id,
                    ParentId = child.ParentId,
                    Name = child.Name,
                    Surname = child.Surname,
                    IDNumber = child.IDNumber
                };
                _children.Add(_child);
            }
            return View(_children);
        }

        [HttpGet]
        public IActionResult EditChild(int Id)
        {
            ChildModel childModel = childDb.GetRow(Id);
            Models.ChildModel _childModel;
            if(childModel != null) {
                _childModel = new Models.ChildModel {
                    Id = childModel.Id,
                    ParentId = childModel.ParentId,
                    Name = childModel.Name,
                    Surname = childModel.Surname,
                    IDNumber = childModel.IDNumber
                };
                return View(_childModel);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public IActionResult EditChild(Models.ChildModel childModel)
        {
            if(ModelState.IsValid) {
                ChildModel _childModel = new ChildModel {
                    Id = childModel.Id,
                    ParentId = childModel.ParentId,
                    Name = childModel.Name,
                    Surname = childModel.Surname,
                    IDNumber = childModel.IDNumber

                };
                childDb.SetRow(_childModel);
                return RedirectToAction("ChildDetails",new {ParentId = _childModel.ParentId });
            }
            return View("Error");
        }


        [HttpPost]
        public IActionResult ParentDetails(Models.ParentModel parentModel)
        {
            if (ModelState.IsValid) {

                ParentModel _parentModel = new ParentModel {
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
            ParentModel model = parentDb.GetRow(Id);
            if (model != null) {
                Models.ParentModel RegisteredParent = new Models.ParentModel {
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
            ParentModel model = parentDb.GetRow(Id);
            if (model != null) {
                Models.ParentModel RegisteredParent = new Models.ParentModel {
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
        public IActionResult EditParent(Models.ParentModel parentModel)
        {
            if (ModelState.IsValid) {

                ParentModel model = new ParentModel {
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
        public IActionResult CreateChild(int Id)
        {
            ChildModel childModel = new ChildModel() { Id = 1,
                ParentId = Id };
            return View(childModel);
        }

        [HttpPost]
        public IActionResult CreateChild(Models.ChildModel childModel)
        {
            if (ModelState.IsValid) {
                ChildModel child = new ChildModel {
                    Id = childModel.Id,
                    ParentId = childModel.ParentId,
                    Name = childModel.Name,
                    Surname = childModel.Surname,
                    IDNumber = childModel.IDNumber };

                childDb.SetRow(child);
                PaymentModel paymentModel = new PaymentModel {
                    ParentId = childModel.ParentId,
                    Amount = PaymentPerChild
                };
                paymentDb.SetRow(paymentModel);
                return RedirectToAction("ChildDetails", childModel);
            }


            return RedirectToAction("Error");
        }

        [HttpGet]
        [Route("/{Id?}/{ParentId?}")]
        public IActionResult DeleteChild(int Id,int ParentId)
        {
            childDb.DeleteRow(Id);
            return RedirectToAction("ChildDetails",new { Id = ParentId });
        }

        [HttpGet]
        public IActionResult Registration()
        {
            //do a search for registered parent
            //if found redirect to parent details
            ParentModel model = parentDb.GetRow(User.Identity.Name);
            
            if (model != null) {
                //Found a registered parent
               
                return RedirectToAction("ParentDetails",new { Id = model.Id});
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

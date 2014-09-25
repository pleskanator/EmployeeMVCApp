using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MikesApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            GetUserInfo(null, null);
            return View("UserInfo");
        }

        public PartialViewResult GetUserInfo(string firstName, string lastName)
        {
            
            MikesApp.SVC.User.UserClient svc = new SVC.User.UserClient();
            SVC.User.Employee[] employees;
            employees = svc.GetEmployeeInfo(firstName, lastName);
            
            return PartialView(Url.Content("~/Views/Home/EmployeeInformation.cshtml"), employees);
        }

        public ActionResult SaveUserInfo(Models.EmployeeInfo employeeInfo)
        {
            MikesApp.SVC.User.UserClient svc = new SVC.User.UserClient();
            
            return Json(svc.SaveEmployeeInfo(employeeInfo.FirstName, employeeInfo.LastName, employeeInfo.Address, employeeInfo.Email, employeeInfo.Phone), JsonRequestBehavior.AllowGet) ;
            
        }

    }
}

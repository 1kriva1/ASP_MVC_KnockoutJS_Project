using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Educat.Model;
using Educat.Logic;
using Educat.Logic.Dto;
using Educat.Logic.Pages;
using Educat.Model.Table;

namespace Educat.Web.Controllers
{
    public class HomeController : Controller
    {      
        public ActionResult Student(int ? id)
        {
            var model = new StudentDto();
            if (id != null)
            {
                model = DataService.Student((int)id);
            }
            return View(model);            
        }

        public ActionResult Students()
        {
            return View();
        }

        public ActionResult School(int? id)
        {
            var model = new School();
            TempData["SchoolId"] = id;
            if (id != null)
            {
                model = DataService.School((int)id);
            }
            return View(model);
        }

        public ActionResult Schools()
        {
            return View();
        }

        public ActionResult Subject(int? id)
        {
            var model = new SubjectPage();
            if (id != null)
            {
                model = DataService.Subject((int)id);
            }
            return View(model);
        }

        public ActionResult Subjects()
        {
            return View();
        }

        public ActionResult Cource(int? id)
        {
            var model = new CourcePage();
            if (id != null)
            {
                model = DataService.Cource((int)id);
            }
            else
            {
                model.StartDateString = "1/1/2015";
                model.FinishDateString = "1/1/2015";
                model.SchoolId = Convert.ToInt32(TempData["SchoolId"]);
                //model.SubjectId = Convert.ToInt32(TempData["SubjectId"]);
            }            
            return View(model);
        }

        public ActionResult Cources()
        {
            return View();
        }
    }
}

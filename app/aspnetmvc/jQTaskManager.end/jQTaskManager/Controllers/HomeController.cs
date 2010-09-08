using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using jQTaskManager.Models;

namespace jQTaskManager.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private edc dc = new edc();
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return Json(
                    dc
                        .Tasks
                        .Select(a => new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Priority = a.Priority1.Name,
                            StartDate = a.StartDate.ToShortDateString(),
                            DueDate = a.DueDate.ToShortDateString(),
                            HoursSpent = a.HoursSpent,
                            HoursRemaining = a.HoursRemaining
                        }),
                        JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }

        public JsonResult Task(int id)
        {
            return Json(
                    dc
                        .Tasks
                        .Where( a => a.Id == id)
                        .Select(a => new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Priority = a.Priority1.Name,
                            StartDate = a.StartDate.ToShortDateString(),
                            DueDate = a.DueDate.ToShortDateString(),
                            HoursSpent = a.HoursSpent,
                            HoursRemaining = a.HoursRemaining
                        }),
                        JsonRequestBehavior.AllowGet);
        }

        public bool Remove(int id)
        {
            dc
                .Tasks
                .DeleteOnSubmit(
                    dc
                        .Tasks
                        .Where(a => a.Id == id)
                        .FirstOrDefault());
                

            dc.SubmitChanges();

            return true;
        }

        [HttpPost]
        public bool Update(FormCollection updateForm)
        {

            var taskToUpdate = dc
                            .Tasks
                            .Where(a => a.Id == Convert.ToInt32(updateForm["Id"]))
                            .FirstOrDefault();
            taskToUpdate.DueDate = Convert.ToDateTime(updateForm["DueDate"]);
            taskToUpdate.Name = updateForm["Name"];
            taskToUpdate.Priority1 = dc.Priorities
                .Where(a => a.Name == updateForm["Priority"])
                .First();
            taskToUpdate.StartDate = Convert.ToDateTime(updateForm["StartDate"]);
            taskToUpdate.HoursRemaining = Convert.ToDecimal(updateForm["HoursRemaining"]);
            taskToUpdate.HoursSpent = Convert.ToDecimal(updateForm["HoursSpent"]);
            dc.SubmitChanges();
            return true;

        }

        [HttpPost]
        public bool Create(FormCollection newTask)
        {
            var newTaskModel = new Models.Task
            {
                DueDate = Convert.ToDateTime(newTask["DueDate"]),
                Name = newTask["Name"],
                Priority1 = dc.Priorities
                    .Where(a => a.Name == newTask["Priority"])
                    .First(),
                StartDate = Convert.ToDateTime(newTask["StartDate"]),
                HoursRemaining = Convert.ToDecimal(newTask["HoursRemaining"]),
                HoursSpent = Convert.ToDecimal(newTask["HoursSpent"])
            };
            dc.Tasks.InsertOnSubmit(newTaskModel);
            dc.SubmitChanges();
            return true;
        }

        public JsonResult Priorities()
        {
            return Json(dc.Priorities
                .Select(a => a.Name)
                , JsonRequestBehavior.AllowGet);
        }
    }
}

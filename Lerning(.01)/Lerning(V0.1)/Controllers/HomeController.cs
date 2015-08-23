using Lerning_V0._1_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lerning_V0._1_.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // создаем контекст данных
        TaskContext db = new TaskContext();

        public ActionResult Teacher()
        {
            // получаем из бд все объекты Book
            IEnumerable<Task> tasks = db.TaskList;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.TaskList = tasks;
            // возвращаем представление
            return View();
            
        }

        [HttpPost]
        public ActionResult Teacher(Task task)
        {
            task.Date = DateTime.Now;
            db.TaskList.Add(task);
            db.SaveChanges();
            return RedirectToAction("Teacher");
        }

        
        public ActionResult Student()
        {
            // получаем из бд все объекты Book
            IEnumerable<Task> tasks = db.TaskList;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.TaskList = tasks;
            // возвращаем представление
            return View();
        }

       
    }
}
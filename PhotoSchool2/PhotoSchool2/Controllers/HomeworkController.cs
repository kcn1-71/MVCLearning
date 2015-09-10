using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhotoSchool2.Models;

namespace PhotoSchool2.Controllers
{
    public class HomeworkController : Controller
    {
        IRepository<Homework> db;
        public HomeworkController()
        {
            db = new HomeworkRepository();
        }


        // GET: Homework
        public ActionResult Index()
        {
            if (User.IsInRole("admin")) return Redirect("/Homework/IndexAdmin");
            else if (User.IsInRole("user")) return Redirect("/Homework/IndexUser");
            else return View(db.GetList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult IndexAdmin()
        {
            return View(db.GetList());
        }

        [Authorize(Roles = "user")]
        public ActionResult IndexUser()
        {
            return View(db.GetList());
        }

        // GET: Homework/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Get(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        // GET: Homework/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Homework/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,NumberOfPhotos,Text")] Homework homework, HttpPostedFileBase[] fileUpload)
        {
            if (ModelState.IsValid)
            {
                db.Create(homework, fileUpload);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(homework);
        }

        // GET: Homework/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Get(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,NumberOfPhotos,Text,Date")] Homework homework, string[] DeleteCheckBox, HttpPostedFileBase[] fileUpload)
        {
            if (ModelState.IsValid)
            {
                db.Update(homework, DeleteCheckBox, fileUpload);
                db.Save();
                return RedirectToAction("Index");
            }
            return View(homework);
        }

        // GET: Homework/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Get(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Homework homework = db.Get(id);
            db.Delete(homework);
            db.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

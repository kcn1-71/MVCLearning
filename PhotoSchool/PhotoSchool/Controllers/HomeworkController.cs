using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhotoSchool.Models;
using System.IO;

namespace PhotoSchool.Controllers
{
    public class HomeworkController : Controller
    {
        private HomeworkContext db = new HomeworkContext();

        // GET: Homework
        public ActionResult Index()
        {
            return View(db.Homeworks.ToList());
        }

        // GET: Homework/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Homeworks.Find(id);
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
        public ActionResult Create([Bind(Include = "Id,Text,Date")] Homework homework, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            if (ModelState.IsValid)
            {

                // Сохраняем файл в папку----------------------------------------------------
                foreach (var file in fileUpload)
                {
                    if (file == null) continue;
                    string path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles/";
                    string filename = Path.GetFileName(file.FileName);
                    if (filename != null) file.SaveAs(Path.Combine(path, filename));

                    HomeworksPhoto NewPhoto = new HomeworksPhoto();
                    NewPhoto.Name = file.FileName;
                    NewPhoto.HomeworksPhotoPath = Path.Combine(path, filename);
                    NewPhoto.HomeworksId = homework.Id;
                    db.HomeworksPhotoList.Add(NewPhoto);
                    db.SaveChanges();
                }
                // --------------------------------------------------------------------------

                homework.Date = DateTime.Now;
                db.Homeworks.Add(homework);
                db.SaveChanges();
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
            Homework homework = db.Homeworks.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Text,Date")] Homework homework)
        {
            if (ModelState.IsValid)
            {

                db.Entry(homework).State = EntityState.Modified;
                db.SaveChanges();
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
            Homework homework = db.Homeworks.Find(id);
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
            Homework homework = db.Homeworks.Find(id);
            db.Homeworks.Remove(homework);
            db.SaveChanges();
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

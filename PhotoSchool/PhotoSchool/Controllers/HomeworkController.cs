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

            ViewBag.Photos = homework.Photos;
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
        public ActionResult Create([Bind(Include = "Id,Text,Date")] Homework homework, HttpPostedFileBase[] fileUpload)
        {
            if (ModelState.IsValid)
            {
                // Сначала важно вставить запись самого задания, только потом фото, т.к. оно ссылается на задание
                homework.Date = DateTime.Now;
                db.Homeworks.Add(homework);

                // Сохраняем файл в папку----------------------------------------------------
                foreach (var file in fileUpload)
                {
                    if (file == null) continue;
                    var path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var filename = Path.GetFileName(file.FileName);
                    if (filename != null) file.SaveAs(Path.Combine(path, filename));

                    var newPhoto = new HomeworksPhoto
                    {
                        Name = file.FileName,
                        HomeworksPhotoPath = "/UploadedFiles/" + filename,   // Нам не нужен путь на диске! Только относительный путь на сайте
                        HomeworkId = homework.Id
                    };
                    db.HomeworksPhotoList.Add(newPhoto);
                }
                // --------------------------------------------------------------------------

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
            ViewBag.Photos = homework.Photos;
            return View(homework);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,Date")] Homework homework, string[] DeleteCheckBox, HttpPostedFileBase[] fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (DeleteCheckBox != null && DeleteCheckBox.Length > 0)
                {
                    foreach (string DelPhoto in DeleteCheckBox)
                    {
                        HomeworksPhoto photo = db.HomeworksPhotoList.Find(Convert.ToInt32(DelPhoto));
                        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + photo.HomeworksPhotoPath))
                        {
                            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + photo.HomeworksPhotoPath);
                        }
                        db.HomeworksPhotoList.Remove(photo);
                    }
                }
                // Сохраняем файл в папку----------------------------------------------------
                foreach (var file in fileUpload)
                {
                    if (file == null) continue;
                    var path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var filename = Path.GetFileName(file.FileName);
                    if (filename != null) file.SaveAs(Path.Combine(path, filename));

                    var newPhoto = new HomeworksPhoto
                    {
                        Name = file.FileName,
                        HomeworksPhotoPath = "/UploadedFiles/" + filename,   // Нам не нужен путь на диске! Только относительный путь на сайте
                        HomeworkId = homework.Id
                    };
                    db.HomeworksPhotoList.Add(newPhoto);
                }
                // --------------------------------------------------------------------------


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
            ViewBag.Photos = homework.Photos;
            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Homework homework = db.Homeworks.Find(id);

            foreach (var photo in homework.Photos)
            {
                if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + photo.HomeworksPhotoPath))
                {
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + photo.HomeworksPhotoPath);
                }
            }
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

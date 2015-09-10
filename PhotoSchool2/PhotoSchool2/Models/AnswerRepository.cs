using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoSchool2.Models
{
    public class AnswerRepository : IRepository<Answer>
    {
        private HomeworkContext _db;
        public AnswerRepository()
        {
            this._db = new HomeworkContext();
        }

        public IEnumerable<Answer> GetList()
        {
            return _db.Answers;
        }

        public Answer Get(int? id)
        {
            return _db.Answers.Find(id);
        }

        public Answer Get(int id)
        {
            return _db.Answers.Find(id);
        }

        public void Create(Answer answer, HttpPostedFileBase[] fileUpload)
        {
            answer.Date = DateTime.Now;
            _db.Answers.Add(answer);

            foreach (var file in fileUpload)
            {
                if (file == null) continue;
                var path = AppDomain.CurrentDomain.BaseDirectory + "AnswerUploadedFiles/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = Path.GetFileName(file.FileName);
                if (filename != null) file.SaveAs(Path.Combine(path, filename));

                var newPhoto = new AnswerPhoto
                {
                    Name = file.FileName,
                    Path = "/AnswerUploadedFiles/" + filename,
                    AnswerId = answer.Id
                };

                _db.AnswerPhotoList.Add(newPhoto);
            }
        }

        public void Update(Answer answer, string[] DeleteCheckBox, HttpPostedFileBase[] fileUpload)
        {
            //if (DeleteCheckBox != null && DeleteCheckBox.Length > 0)
            //{
            //    foreach (string DelPhoto in DeleteCheckBox)
            //    {
            //        HomeworkPhoto photo = _db.HomeworkPhotoList.Find(Convert.ToInt32(DelPhoto));
            //        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + photo.Path))
            //        {
            //            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + photo.Path);
            //        }
            //        _db.HomeworkPhotoList.Remove(photo);
            //    }
            //}

            //foreach (var file in fileUpload)
            //{
            //    if (file == null) continue;
            //    var path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles/";
            //    if (!Directory.Exists(path))
            //    {
            //        Directory.CreateDirectory(path);
            //    }

            //    var filename = Path.GetFileName(file.FileName);
            //    if (filename != null) file.SaveAs(Path.Combine(path, filename));

            //    var newPhoto = new HomeworkPhoto
            //    {
            //        Name = file.FileName,
            //        Path = "/UploadedFiles/" + filename,   // Нам не нужен путь на диске! Только относительный путь на сайте
            //        HomeworkId = homework.Id
            //    };
            //    _db.HomeworkPhotoList.Add(newPhoto);
            //}
            _db.Entry(answer).State = EntityState.Modified;
        }

        public void Delete(Answer answer)
        {
            foreach (var photo in answer.Photos)
            {
                if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + photo.Path))
                {
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + photo.Path);
                }
            }

            if (answer != null)
                _db.Answers.Remove(answer);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
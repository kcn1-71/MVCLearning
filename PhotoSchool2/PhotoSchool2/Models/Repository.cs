using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoSchool2.Models
{
    interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetList(); // получение всех объектов
        T Get(int? id); // получение одного объекта по id
        T Get(int id);
        void Create(T item, HttpPostedFileBase[] array); // создание объекта
        void Update(T item, string[] DeleteCheckBox, HttpPostedFileBase[] fileUpload); // обновление объекта
        void Delete(T item);
        void Save();  // сохранение изменений
    }

    public class HomeworkRepository : IRepository<Homework>
    {
        private HomeworkContext _db;
        public HomeworkRepository()
        {
            this._db = new HomeworkContext();
        }

        public IEnumerable<Homework> GetList()
        {
            return _db.Homeworks;
        }

        public Homework Get(int? id)
        {
            return _db.Homeworks.Find(id);
        }

        public Homework Get(int id)
        {
            return _db.Homeworks.Find(id);
        }

        public void Create(Homework homework, HttpPostedFileBase[] fileUpload)
        {
            homework.Date = DateTime.Now;
            _db.Homeworks.Add(homework);

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

                var newPhoto = new HomeworkPhoto
                {
                    Name = file.FileName,
                    Path = "/UploadedFiles/" + filename,
                    HomeworkId = homework.Id
                };

                _db.HomeworkPhotoList.Add(newPhoto);
            }
        }

        public void Update(Homework homework, string[] DeleteCheckBox, HttpPostedFileBase[] fileUpload)
        {
            if (DeleteCheckBox != null && DeleteCheckBox.Length > 0)
            {
                foreach (string DelPhoto in DeleteCheckBox)
                {
                    HomeworkPhoto photo = _db.HomeworkPhotoList.Find(Convert.ToInt32(DelPhoto));
                    if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + photo.Path))
                    {
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + photo.Path);
                    }
                    _db.HomeworkPhotoList.Remove(photo);
                }
            }

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

                var newPhoto = new HomeworkPhoto
                {
                    Name = file.FileName,
                    Path = "/UploadedFiles/" + filename,   // Нам не нужен путь на диске! Только относительный путь на сайте
                    HomeworkId = homework.Id
                };
                _db.HomeworkPhotoList.Add(newPhoto);
            }
            _db.Entry(homework).State = EntityState.Modified;
        }

        public void Delete(Homework homework)
        {
            foreach (var photo in homework.Photos)
            {
                if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + photo.Path))
                {
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + photo.Path);
                }
            }

            if (homework != null)
                _db.Homeworks.Remove(homework);
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
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
        void Update(T item); // обновление объекта
        void Delete(T item);
        void Save();  // сохранение изменений
    }

    public class HomeworkRepository : IRepository<Homework>
    {
        private HomeworkContext db;
        public HomeworkRepository()
        {
            this.db = new HomeworkContext();
        }

        public IEnumerable<Homework> GetList()
        {
            return db.Homeworks;
        }

        public Homework Get(int? id)
        {
            return db.Homeworks.Find(id);
        }

        public Homework Get(int id)
        {
            return db.Homeworks.Find(id);
        }

        public void Create(Homework homework, HttpPostedFileBase[] fileUpload)
        {
            homework.Date = DateTime.Now;
            db.Homeworks.Add(homework);

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

                db.HomeworkPhotoList.Add(newPhoto);
            }
        }

        public void Update(Homework homework)
        {
            db.Entry(homework).State = EntityState.Modified;
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
                db.Homeworks.Remove(homework);
        }      

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
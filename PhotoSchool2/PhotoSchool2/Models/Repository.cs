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
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSchool2.Models
{
    public class HomeworkContext : DbContext
    {
        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<HomeworkPhoto> HomeworkPhotoList { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<AnswerPhoto> AnswerPhotoList { get; set; }
    }
}
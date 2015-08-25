using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSchool.Models
{
    public class HomeworkContext : DbContext
    {
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<HomeworksPhoto> HomeworksPhotoList { get; set; }
    }
}
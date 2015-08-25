using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSchool.Models 
{
    public class HomeworksPhotoContext : DbContext
    {
        public HomeworksPhotoContext()
        : base("DefaultConnection")
    { }

        public DbSet<HomeworksPhoto> HomeworksPhotoList { get; set; }
    }
}
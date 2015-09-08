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
    }
}
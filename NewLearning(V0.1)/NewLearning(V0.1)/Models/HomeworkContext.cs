using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewLearning_V0._1_.Models
{
    public class HomeworkContext : DbContext
    {
        public DbSet<Homework> HomeworkList { get; set; }
    }
}
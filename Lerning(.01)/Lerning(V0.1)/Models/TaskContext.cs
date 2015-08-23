using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lerning_V0._1_.Models
{
    public class TaskContext : DbContext
    {
        public DbSet<Task> TaskList { get; set; }
    }
}
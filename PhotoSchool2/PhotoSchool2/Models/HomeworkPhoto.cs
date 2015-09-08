using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoSchool2.Models
{
    public class HomeworkPhoto
    {
        [Key]
        public int Id { get; set; }

        public int HomeworkId { get; set; }
        public Homework Homework { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoSchool.Models
{
    public class Homework
    {
        public int HomeworkId { get; set; }
        public string HomeworkText { get; set; }
        public DateTime HomeworkDateTime { get; set; }
    }
}
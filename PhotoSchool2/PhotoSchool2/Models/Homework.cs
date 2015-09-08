using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoSchool2.Models
{
    public class Homework
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int NumberOfPhotos { get; set; }
        public string Text { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
    }
}
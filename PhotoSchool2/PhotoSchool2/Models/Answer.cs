using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoSchool2.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HomeworkId { get; set; }
        public string Text { get; set; }

        public Homework Homework { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        public virtual ICollection<AnswerPhoto> Photos { get; set; }
    }
}
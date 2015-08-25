using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoSchool.Models
{
    public class HomeworksPhoto
    {
        [Key]
        public int HomeworksPhotoId { get; set; }

        public int HomeworksId { get; set; }
        public string Name { get; set; }
        public string HomeworksPhotoPath { get; set; }
    }
}
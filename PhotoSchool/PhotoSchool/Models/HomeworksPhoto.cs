using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoSchool.Models
{
    public class HomeworksPhoto
    {
        public int HomeworksPhotoId { get; set; }
        public string Name { get; set; } // название картинки
        public byte[] Image { get; set; }
    }
}
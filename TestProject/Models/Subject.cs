using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImgURL { get; set; }
        public string PageUrl { get; set; }
        public int QuestionCount { get; set; }
        public string Type { get; set; }
    }
}

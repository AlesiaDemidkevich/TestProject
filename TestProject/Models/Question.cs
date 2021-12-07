using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public int IdSubject { get; set; }
        public int IdTest { get; set; }
        public string Type { get; set; }
    }
}

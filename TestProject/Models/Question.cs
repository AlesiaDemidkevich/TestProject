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
        public int IdQuestionCoefficient { get; set; }
        public string ImageUrl { get; set; }
        public int IdTest { get; set; }
    }
}

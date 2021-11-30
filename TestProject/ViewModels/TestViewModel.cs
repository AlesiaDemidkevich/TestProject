using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.ViewModels
{
    public class TestViewModel
    {
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Question text")]
        public string QuestionText { get; set; }

        //[Required]
        //[Display(Name = "Answer text")]
        //public string AnswerText { get; set; }

        //[Required]
        //[Display(Name = "Question coefficient")]
        //public int QuestionCoefficient { get; set; }
        //public bool isRight { get; set; }
        //public string ImageUrl { get; set; }
        public ICollection<QuestionAnswer> AnswerList { get; set; }

    }
}

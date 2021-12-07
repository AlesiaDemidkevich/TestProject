using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.ViewModels
{
    public class QuestionViewModel{
        
        [Required]
        [Display(Name = "Question text")]
        public string Text { get; set; }

        public string Type { get; set; }
        public ICollection<QuestionAnswer> AnswerList { get; set; }

        public string ImageUrl { get; set; }
        public IFormFile ImageUrlFile { get; set; }

    }
}

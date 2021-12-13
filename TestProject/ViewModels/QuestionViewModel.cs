using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public List<QuestionAnswerViewModel> AnswerList { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile ImageUrlFile { get; set; }

    }
}

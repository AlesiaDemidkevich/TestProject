using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Variant { get; set; }
        public string Type { get; set; }
        public int IdTest { get; set; }

        [NotMapped]
        public List<QuestionViewModel> QuestionList { get; set; }

    }
}

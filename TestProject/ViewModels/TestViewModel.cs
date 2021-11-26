using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.ViewModels
{
    public class TestViewModel
    {
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string Question { get; set; }

    }
}

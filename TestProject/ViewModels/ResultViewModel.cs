using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.ViewModels
{
    public class ResultViewModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        [NotMapped]
        public TestViewModel test { get; set; }
        public int IdTest { get; set; }

        public string Subject { get; set; }
        public string Variant { get; set; }
        public double Mark { get; set; }

        public FileInfo Url { get; set; }
        public DateTime Date { get; set; }
    }
}

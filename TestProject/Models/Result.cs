using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TestProject.ViewModels;

namespace TestProject.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        [NotMapped]
        public TestViewModel test { get; set; }
        public double Mark { get; set; }
        public DateTime Date { get; set; }
    }
}

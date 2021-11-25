using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdUserAnswer { get; set; }
        public int IdTest { get; set; }
        public float Mark { get; set; }
        public DateTime Date { get; set; }
    }
}

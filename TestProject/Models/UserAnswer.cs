using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int IdQuestionAnswer { get; set; }
        public string ChoosedAnswer { get; set; }
    }
}

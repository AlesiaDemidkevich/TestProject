using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.ViewModels
{
    public class QuestionAnswerViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int IdQuestion { get; set; }
        public bool isRight { get; set; } = false;
        public bool isChecked { get; set; } = false;
        public string UserText { get; set; }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.pdf.draw;
using System.Globalization;
using DocumentFormat.OpenXml.Vml;
using iText.IO.Image;
using Rectangle = iTextSharp.text.Rectangle;

namespace TestProject.Controllers
{
    public class UserTestController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        IWebHostEnvironment _appEnvironment;
        public string getCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        public UserTestController(ILogger<HomeController> logger, ApplicationContext context, IWebHostEnvironment appEnvironment, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            db = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public IActionResult Index(int subId, int variant)
        {
            if (User.IsInRole("user"))
            {
                TestViewModel testViewModel = GetTest(subId, variant);
                ViewBag.testView = testViewModel;
                return View("UserTest");
            }
            else
            {
                return StatusCode(403);
            }
            }

        public IActionResult ShowResult(int idR)        {

            if (User.IsInRole("user"))
            {
                string idUser = getCurrentUserId();
                var res = db.Results.Where(t => t.IdUser == idUser).ToList();
                List<ResultViewModel> results = new List<ResultViewModel>();
                foreach (var r in res)
                {
                    var subject = (from sub in db.Subjects
                                   join test in db.Tests on sub.Id equals test.IdSubject
                                   where test.Id == r.IdTest
                                   select sub).First().Name;

                    var variant = (from var in db.Variants
                                   join test in db.Tests on var.Id equals test.IdVariant
                                   where test.Id == r.IdTest
                                   select var).First().Name;
                    ResultViewModel result = new ResultViewModel { Id = r.Id, IdUser = r.IdUser, IdTest = r.IdTest, Subject = subject, Variant = variant, Mark = r.Mark, Date = r.Date };
                    results.Add(result);

                    foreach (var ress in results)
                    {
                        if (ress.Id == idR)
                        {
                            string idU = getCurrentUserId();
                            string subjectAbb = "";
                            switch (ress.Subject)
                            {
                                case "Английский язык":
                                    subjectAbb = "En";
                                    break;
                                case "Беларуская мова":
                                    subjectAbb = "Bel";
                                    break;
                                case "Биология":
                                    subjectAbb = "Biol";
                                    break;
                                case "Химия":
                                    subjectAbb = "Chem";
                                    break;
                                case "История Беларуси":
                                    subjectAbb = "His";
                                    break;
                                case "Математика":
                                    subjectAbb = "Math";
                                    break;
                                case "Обществоведение":
                                    subjectAbb = "Obsh";
                                    break;
                                case "Русский язык":
                                    subjectAbb = "Rus";
                                    break;
                                case "Физика":
                                    subjectAbb = "Phis";
                                    break;
                                default:
                                    break;
                            }
                            var Name = db.Users.Where(s => s.Id == idU).First().UserName;
                            DateTime date = ress.Date;
                            string dd = date.ToString("g", CultureInfo.GetCultureInfo("de-DE"));
                            dd = dd.Replace(':', '.');
                            dd = dd.Replace(" ", "_");
                            String vv = ress.Variant.Substring(8);
                            DirectoryInfo di = new DirectoryInfo(_appEnvironment.WebRootPath + "/result");
                            FileInfo[] rgFile = di.GetFiles().Where(s => s.Name.Contains($"{Name}_{subjectAbb}_{vv}_{dd}.pdf")).ToArray();
                            ViewBag.filesToLoad = rgFile;                           

                            return View("LoadData");
                        }
                    }
                }
                return View("LoadData");
            }
            else
            {
                return StatusCode(403);
            }
            
        }


        public TestViewModel GetTest(int subId, int variant) {
            var subject = db.Subjects.Where(s => s.Id == subId).First().Name;
            var varName = db.Variants.Where(v => v.Id == variant).First().Name;
            var testId = db.Tests.Where(t => t.IdSubject == subId && t.IdVariant == variant).First().Id;
            var questions = db.Questions.Where(q => q.IdSubject == subId && q.IdTest == testId).ToList();

            List<QuestionViewModel> qwList = new List<QuestionViewModel>();
            
           
            foreach (var k in questions)
            {
                List<QuestionAnswerViewModel> qwestionAnswerList = new List<QuestionAnswerViewModel>();
                var answers = (from answ in db.QuestionAnswers
                               join quest in db.Questions on answ.IdQuestion equals quest.Id
                               join test in db.Tests on quest.IdTest equals test.Id
                               where test.Id == testId && quest.Id == k.Id
                               select answ).ToList();

                foreach (var a in answers)
                {
                    QuestionAnswerViewModel item = new QuestionAnswerViewModel();
                    item.Id = a.Id;
                    item.IdQuestion = a.IdQuestion;
                    item.isChecked = a.isChecked;
                    item.isRight = a.isRight;
                    item.Text = a.Text;
                    item.UserText = a.UserText;
                    qwestionAnswerList.Add(item);

                }
                qwList.Add(new QuestionViewModel { Text = k.Text, ImageUrl = k.ImageUrl, Type = k.Type, AnswerList = qwestionAnswerList });
            }
            

                TestViewModel testViewModel = new TestViewModel { Subject = subject, Variant = varName, QuestionList = qwList, IdTest = testId };
            return testViewModel;
        }

        public async Task<IActionResult> Test(TestViewModel testViewModel, string subject, string variant)
        {
            if (User.IsInRole("user"))
            {
                double mark = 0;
            double coef = 0;
            double count = 0;
            var subId = db.Subjects.Where(s => s.Name == subject).First().Id;
            var varId = db.Variants.Where(v => v.Name == variant).First().Id;
            TestViewModel allTestViewModel = GetTest(subId, varId);
            allTestViewModel = SortTest(allTestViewModel);
            TestViewModel userTestViewModel = allTestViewModel;

            for (int i = 0; i < allTestViewModel.QuestionList.Count; i++)
            {
                
                for (int j = 0; j < allTestViewModel.QuestionList[i].AnswerList.Count; j++)
                {
                    if (allTestViewModel.QuestionList[i].AnswerList[j].isRight == true)
                    {
                        count++;
                    }
                }

            }
            coef = 100 / count; 
            for (int i=0;i<allTestViewModel.QuestionList.Count;i++){

                for (int j = 0; j < allTestViewModel.QuestionList[i].AnswerList.Count; j++) {
                    if (allTestViewModel.QuestionList[i].Type == "A") {
                        if (testViewModel.QuestionList[i].AnswerList[j].isChecked && allTestViewModel.QuestionList[i].AnswerList[j].isRight) {
                            mark += coef;
                        }
                    }
                    if (allTestViewModel.QuestionList[i].Type == "B") {
                        if (testViewModel.QuestionList[i].AnswerList[0].Text != null) { 
                                if (allTestViewModel.QuestionList[i].AnswerList[0].Text.ToUpper().Equals(testViewModel.QuestionList[i].AnswerList[0].Text.ToUpper())){
                                     mark += coef;
                                }
                        }
                    }
                }

            }

            for (int i = 0; i < allTestViewModel.QuestionList.Count; i++)
            {

                for (int j = 0; j < allTestViewModel.QuestionList[i].AnswerList.Count; j++)
                {
                    if (allTestViewModel.QuestionList[i].Type == "A")
                    {
                        if (testViewModel.QuestionList[i].AnswerList[j].isChecked)
                        {
                            allTestViewModel.QuestionList[i].AnswerList[j].isChecked = true;
                        }
                    }
                    if (allTestViewModel.QuestionList[i].Type == "B")
                    {
                        allTestViewModel.QuestionList[i].AnswerList[0].UserText = testViewModel.QuestionList[i].AnswerList[0].Text;
                        
                    }
                }

            }

            string idUser = getCurrentUserId();
            DateTime date = DateTime.Now;

            ResultViewModel resultVM = new ResultViewModel {Mark = Math.Round(mark, MidpointRounding.AwayFromZero),test = allTestViewModel, IdTest = allTestViewModel.IdTest, IdUser = idUser, Date = date};
            ViewBag.result = resultVM;

                //allTestViewModel = userTestViewModel;
                Result result = new Result { Mark = Math.Round(mark, MidpointRounding.AwayFromZero), IdUser = idUser, Date = date, IdTest = allTestViewModel.IdTest };
            db.Results.Add(result);
            await db.SaveChangesAsync();

                savePdf(allTestViewModel, result, resultVM, subject);
                allTestViewModel = userTestViewModel;
                return View("Result", ViewBag);
            }
            else
            {
                return StatusCode(403);
            }

        }

        public void savePdf(TestViewModel test, Result result, ResultViewModel res, string subject)
        {
            int i = 0;
            int k = 0;
            int a = 0;
            int b = 0;
            int num2 = 0;
            string subjectAbb = "";
            switch (subject){
                case "Английский язык":
                    subjectAbb = "En";
                        break;
                case "Беларуская мова":
                    subjectAbb = "Bel";
                    break;
                case "Биология":
                    subjectAbb = "Biol";
                    break;
                case "Химия":
                    subjectAbb = "Chem";
                    break;
                case "История Беларуси":
                    subjectAbb = "His";
                    break;
                case "Математика":
                    subjectAbb = "Math";
                    break;
                case "Обществоведение":
                    subjectAbb = "Obsh";
                    break;
                case "Русский язык":
                    subjectAbb = "Rus";
                    break;
                case "Физика":
                    subjectAbb = "Phis";
                    break;
            }
            var Name = db.Users.Where(s => s.Id == result.IdUser).First().UserName;
            DateTime date = result.Date;
            string d = date.ToString("g", CultureInfo.GetCultureInfo("de-DE"));
            d = d.Replace(':','.');
            d = d.Replace(" ", "_");

            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(_appEnvironment.WebRootPath + $"/result/{Name}_{subjectAbb}_{result.IdTest}_{d}.pdf", FileMode.Create));
            doc.Open();
            _appEnvironment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            BaseFont baseFont = BaseFont.CreateFont(_appEnvironment.WebRootPath + "/fonts/ARIAL.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontErr = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontOk = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontHead = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD);
            Font f = new Font(baseFont,20f,Font.BOLD);
            f.SetColor(60, 179, 113);
            fontOk.SetColor(124, 252, 0);
            fontErr.SetColor(255, 0, 0);
            fontHead.SetColor(60, 179, 113);
            

            Paragraph head = new Paragraph($"{subject}", f);
            head.Alignment = Rectangle.ALIGN_CENTER;

            Paragraph mark = new Paragraph($"Результат - {result.Mark}%", font);
            mark.Alignment = Rectangle.ALIGN_CENTER;

            doc.Add(head);
            doc.Add(mark);
            int num;

            Paragraph partA = new Paragraph($"Часть А", fontHead);
            partA.Alignment = Rectangle.ALIGN_CENTER;
            doc.Add(partA);

            

            foreach (var quest in test.QuestionList)
            {
                if (quest.Type == "A")
                {
                    num = i + 1;
                    Paragraph lab = new Paragraph($"А {num}", font);
                    lab.Alignment = Rectangle.ALIGN_CENTER;
                    doc.Add(lab);
                    Paragraph text = new Paragraph($"{quest.Text}\n", font);
                    text.Alignment = Rectangle.ALIGN_JUSTIFIED;
                    doc.Add(text);
                
                if (quest.ImageUrl != null)
                {
                    Image image = Image.GetInstance(quest.ImageUrl);
                    doc.Add(image);
                }
                    
                    foreach (var answer in quest.AnswerList)
                {
                    if (answer.isChecked && answer.isRight)
                    {
                            Paragraph an = new Paragraph("  \u221A   " + answer.Text,fontOk);                            
                            doc.Add(an);
                            a++;
                    }
                    else if (answer.isChecked && !answer.isRight)
                    {
                            Paragraph an = new Paragraph("  \u221A   " + answer.Text, fontErr);
                            doc.Add(an);
                            a++;
                    }
                    else if (!answer.isChecked && answer.isRight)
                    {
                            Paragraph an = new Paragraph("       " + answer.Text, fontOk);
                            doc.Add(an);
                            a++;
                    }
                    else {
                            Paragraph an = new Paragraph("       " + answer.Text, font);
                            doc.Add(an);
                            a++;
                    }
                }
                a = 0;
                i++;
            }
            }


            Paragraph partB = new Paragraph($"Часть B", fontHead);
            partB.Alignment = Rectangle.ALIGN_CENTER;
            doc.Add(partB);
            foreach (var quest2 in test.QuestionList)
            {
                k = i;
                if (quest2.Type == "B")
                {
                    num2++;
                    Paragraph lab = new Paragraph($"B {num2}", font);
                    lab.Alignment = Rectangle.ALIGN_CENTER;
                    doc.Add(lab);
                    Paragraph text = new Paragraph($"{quest2.Text}\n", font);
                    text.Alignment = Rectangle.ALIGN_JUSTIFIED;
                    doc.Add(text);
                    if (quest2.ImageUrl != null)
                    {
                        Image image = Image.GetInstance(quest2.ImageUrl);
                        doc.Add(image);
                    }
                    foreach (var answer2 in quest2.AnswerList)
                    {   if (answer2.UserText != null)
                        {
                            if (answer2.Text.ToUpper() == answer2.UserText.ToUpper())
                            {
                                Paragraph ans = new Paragraph($"       Ответ:\n", font);
                                doc.Add(ans);
                                Paragraph ans2 = new Paragraph($"       {answer2.Text}", fontOk);
                                doc.Add(ans2);
                            }
                            else
                            {
                                Paragraph ans4 = new Paragraph($"       Ответ:\n", font);
                                doc.Add(ans4);
                                Paragraph ans3 = new Paragraph($"       {answer2.Text}\n", fontOk);
                                doc.Add(ans3);
                                Paragraph ans = new Paragraph($"       Ваш ответ:\n", font);
                                doc.Add(ans);
                                Paragraph ansU = new Paragraph($"       {answer2.UserText}", fontErr);
                                doc.Add(ansU);
                            }
                        }
                        else
                        {
                            Paragraph ans4 = new Paragraph($"       Ответ:\n", font);
                            doc.Add(ans4);
                            Paragraph ans3 = new Paragraph($"       {answer2.Text}\n", fontOk);
                            doc.Add(ans3);
                            Paragraph ans = new Paragraph($"       Ваш ответ:\n", font);
                            doc.Add(ans);
                            Paragraph ansU = new Paragraph($"                  ", fontErr);
                            doc.Add(ansU);
                        }
                    }
                }
                b++;
                k++;
            }

            doc.Close();
        }



        public TestViewModel SortTest(TestViewModel testViewModel){
            List<QuestionViewModel> questionList = new List<QuestionViewModel>();

            foreach (var test in testViewModel.QuestionList)
            {
                if (test.Type == "A")
                {
                    questionList.Add(test);
                }
            }
            foreach (var test in testViewModel.QuestionList)
            {
                if (test.Type == "B")
                {
                    questionList.Add(test);
                }
            }
            testViewModel.QuestionList = questionList;
            return testViewModel;
        }
    }
}

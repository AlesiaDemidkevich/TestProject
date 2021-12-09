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
            TestViewModel testViewModel = GetTest(subId,variant);
            ViewBag.testView = testViewModel;
            return View("UserTest");
        }

        public TestViewModel GetTest(int subId, int variant) {
            var subject = db.Subjects.Where(s => s.Id == subId).First().Name;
            var varName = db.Variants.Where(v => v.Id == variant).First().Name;
            var testId = db.Tests.Where(t => t.IdSubject == subId && t.IdVariant == variant).First().Id;
            var questions = db.Questions.Where(q => q.IdSubject == subId && q.IdTest == testId).ToList();

            List<QuestionViewModel> qwList = new List<QuestionViewModel>();
            foreach (var k in questions)
            {

                var answers = (from answ in db.QuestionAnswers
                               join quest in db.Questions on answ.IdQuestion equals quest.Id
                               join test in db.Tests on quest.IdTest equals test.Id
                               where test.Id == testId && quest.Id == k.Id
                               select answ).ToList();
                qwList.Add(new QuestionViewModel { Text = k.Text, ImageUrl = k.ImageUrl, Type = k.Type, AnswerList = answers });
            }

            TestViewModel testViewModel = new TestViewModel { Subject = subject, Variant = varName, QuestionList = qwList };
            return testViewModel;
        }

        public async Task<IActionResult> Test(TestViewModel testViewModel, string subject, string variant)
        {
            double mark = 0;
            double coef = 0;
            double count = 0;
            var subId = db.Subjects.Where(s => s.Name == subject).First().Id;
            var varId = db.Variants.Where(v => v.Name == variant).First().Id;
            TestViewModel allTestViewModel = GetTest(subId, varId);
            allTestViewModel = SortTest(allTestViewModel);

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
                        if (allTestViewModel.QuestionList[i].AnswerList[j].Text.Equals(testViewModel.QuestionList[i].AnswerList[j].Text))
                        {
                            mark += coef;
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
                        allTestViewModel.QuestionList[i].AnswerList[j].UserText = testViewModel.QuestionList[i].AnswerList[j].Text;
                        
                    }
                }

            }

            
            Result result = new Result {Mark = Math.Round(mark, MidpointRounding.AwayFromZero), test = allTestViewModel };
            ViewBag.result = result;
            return View("Result", ViewBag);
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

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;
using TestProject.ViewModels;

namespace TestProject.Controllers
{
    public class TestController : Controller
    {
        private ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        IWebHostEnvironment _appEnvironment;

        public TestController(ApplicationContext context, IWebHostEnvironment appEnvironment, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            db = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // GET: TestController
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult CreateTest()
        {
            List<Subject> subjectList = db.Subjects.ToList();
            ViewBag.subjects = subjectList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TestViewModel model)
        {            
                
                //Question question = new Question { Text = model.QuestionText, IdQuestionCoefficient=model.QuestionCoefficient,ImageUrl=model.ImageUrl};
                //db.Questions.Add(question);
                await db.SaveChangesAsync();
                if (ModelState.IsValid)
                {
                   // Test test = new Test { IdQuestion = question.Id};
                    //                             db.Tests.Add(test);
                await db.SaveChangesAsync();
                }
                else
                {
                        
                    
                }
            
            return RedirectToAction("CreateTest");
        }
    }
}

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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;

namespace TestProject.Controllers
{
   
    public class TestController : Controller
    {
        Account account = new Account("alesya", "267351316653189", "UPnJYCb_HLWg3_QRcLyJnWCTyyw");        
        private ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        IWebHostEnvironment _appEnvironment;
        private static string DefaultImageUrl = "https://res.cloudinary.com/alesya/image/upload/v1638305940/Images/default-placeholder_akhrf3.png";

        public TestController(ApplicationContext context, IWebHostEnvironment appEnvironment, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            db = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string LoadImage(IFormFile uploadedFile)
        {
            Cloudinary cloudinary = new Cloudinary(account);
            if (uploadedFile != null)
            {
                var uploadParams = new ImageUploadParams()
                {
                    Folder = "Images",
                    File = new FileDescription(uploadedFile.FileName, uploadedFile.OpenReadStream()),
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                return Convert.ToString(uploadResult.Url);
            }
            else return null;
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
            if (ModelState.IsValid)
            {
                int idSubject = db.Subjects.Where(p => p.Name == model.Subject).First().Id;
                int countVariant = db.Tests.Where(v => v.IdSubject == idSubject).Count();
                ICollection<QuestionViewModel> questionList = model.QuestionList;

                Test test = new Test { IdSubject = idSubject, IdVariant = countVariant + 1 };
                db.Tests.Add(test);
                await db.SaveChangesAsync();
                int idTest = db.Tests.OrderByDescending(u => u.Id).First().Id;
                foreach (var i in questionList)
                {
                    var img = LoadImage(i.ImageUrlFile);
                    i.ImageUrl = img;
                    Question question = new Question { Text = i.Text, ImageUrl = i.ImageUrl, IdSubject = idSubject, IdTest = idTest, Type=i.Type };
                    ICollection<QuestionAnswer> questionAnswerList = i.AnswerList;
                    db.Questions.Add(question);
                    await db.SaveChangesAsync();

                    int idQuestion = db.Questions.OrderByDescending(u => u.Id).First().Id;

                    foreach (var a in questionAnswerList)
                    {

                        QuestionAnswer questionAnswer = new QuestionAnswer { Text = a.Text, IdQuestion = idQuestion, isRight = a.isRight };
                        db.QuestionAnswers.Add(questionAnswer);
                        await db.SaveChangesAsync();

                    }

                }
                return RedirectToAction("CreateTest");
            }
            else
            {

                return RedirectToAction("CreateTest");

            }            
            
        }
    }
}

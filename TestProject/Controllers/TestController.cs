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

        
        public ActionResult Index()
        {
            if (User.IsInRole("admin"))
            {                
               return RedirectToAction("Index", "Users", _userManager.Users.ToList());
            }
            else
            {
                return StatusCode(403);
            }
        }

        
        public ActionResult CreateTest(String subName)
        {
            if (User.IsInRole("admin"))
            {
                //List<Subject> subjectList = db.Subjects.ToList();
            ViewBag.subName = subName;
            return View();
            }
            else
            {
                return StatusCode(403);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(TestViewModel model, String subName)
        {
            //if (ModelState.IsValid)
            //{           
                    int idSubject = db.Subjects.Where(p => p.Name == subName).First().Id;
                    int countVariant = db.Tests.Where(v => v.IdSubject == idSubject).Count();
                    List<QuestionViewModel> questionList = model.QuestionList;

                    Test test = new Test { IdSubject = idSubject, IdVariant = countVariant + 1 };
                    db.Tests.Add(test);
                    await db.SaveChangesAsync();
                    int idTest = db.Tests.OrderByDescending(u => u.Id).First().Id;
                    foreach (var i in questionList)
                    {
                        var img = LoadImage(i.ImageUrlFile);
                        i.ImageUrl = img;
                        i.Type = i.Type.Substring(7,1);
                        Question question = new Question { Text = i.Text, ImageUrl = i.ImageUrl, IdSubject = idSubject, IdTest = idTest, Type = i.Type };
                        ICollection<QuestionAnswerViewModel> questionAnswerList = i.AnswerList;
                        db.Questions.Add(question);
                        await db.SaveChangesAsync();

                        int idQuestion = db.Questions.OrderByDescending(u => u.Id).First().Id;


                        if (questionAnswerList != null)
                        {
                            foreach (var a in questionAnswerList)
                               {

                        QuestionAnswer questionAnswer = new QuestionAnswer { Text = a.Text, IdQuestion = idQuestion, isRight = a.isRight };
                        db.QuestionAnswers.Add(questionAnswer);
                        await db.SaveChangesAsync();

                               }
                         }
                    }
                    return RedirectToAction("Index");
                
            //}
            //else
            //{
            //    return RedirectToAction("CreateTest", subName);

            //}
            
        }
    }
}

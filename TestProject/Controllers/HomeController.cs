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
    public class HomeController : Controller
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
        public HomeController(ILogger<HomeController> logger, ApplicationContext context, IWebHostEnvironment appEnvironment, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            db = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
                ViewBag.subjects = db.Subjects.ToList();
                return View();
        }


        [HttpPost]
        public async Task<ActionResult> DeleteResult(int id)
        {
            var result = await db.Results.FindAsync(id);

            if (result != null)
            {
                db.Results.Remove(result);
                await db.SaveChangesAsync();
            }
            
            return RedirectToAction("AllResult");
        }


        public IActionResult AllResult()
        {
            string idUser = getCurrentUserId();
            var res = db.Results.Where(t=>t.IdUser==idUser).ToList();
            List<ResultViewModel> results = new List<ResultViewModel>();
            foreach (var r in res) {
                var subject = (from sub in db.Subjects
                               join test in db.Tests on sub.Id equals test.IdSubject
                               where test.Id == r.IdTest
                               select sub).First().Name;

                var variant = (from var in db.Variants
                               join test in db.Tests on var.Id equals test.IdVariant
                               where test.Id == r.IdTest
                               select var).First().Name;
                ResultViewModel result = new ResultViewModel { Id = r.Id, IdUser = r.IdUser, IdTest = r.IdTest, Subject = subject, Variant = variant, Mark = r.Mark, Date = r.Date};
                results.Add(result);
            }
                       
            ViewBag.results = results;
            return View("UserResult");
        }

        public async Task<IActionResult> GetPage(string URL, int subject)
        {
            ViewBag.variants = db.Tests.Where(a => a.IdSubject == subject).ToList();
            Subject sub = db.Subjects.Where(s => s.Id == subject).First();
            return View(URL,sub);
        }

        public IActionResult Sort(string id)
        {
            if (id == null) {
                id = "All";
            }
            if (id == "All")
            {
                List<Subject> subjects = db.Subjects.ToList();
                int i = 0;
                string cnt = "";
                while (i < subjects.Count)
                {
                    cnt += "<tr>";
                    for (int j = 0; j < 3; j++)
                    {
                        if (i == subjects.Count)
                        {
                            break;
                        }

                        cnt += "<td><a class='cards-item text-center'  href='/Home/GetPage?url=" + subjects[i].PageUrl + "&subject=" + subjects[i].Id + "'><div class='cards-item__img'><img src =" + subjects[i].ImgURL + "></ div ><div class='cards-item__content'><div class='cards-item__name'>" + subjects[i].Name + ". Сборник тестов</div><div class='cards-item__desc'><div class='cards-item__desc-icon'></div><p class='cards-item__desc-text'>5 вариантов теста по " + subjects[i].QuestionCount + " вопросов</p></div></div></a></td>";

                        i++;

                    }
                    cnt += "</tr>";
                }

                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = cnt
                };
            }
            else
            {
                List<Subject> subjects = db.Subjects.Where(t => t.Type == id).ToList();

                int i = 0;
                string cnt = "";
                while (i < subjects.Count)
                {
                    cnt += "<tr>";
                    for (int j = 0; j < 3; j++) {
                        if (i == subjects.Count)
                        {
                            break;
                        }

                        cnt += "<td><a class='cards-item text-center'  href='/Home/GetPage?url=" + subjects[i].PageUrl +"&subject=" + subjects[i].Id + "'><div class='cards-item__img'><img src =" + subjects[i].ImgURL + "></ div ><div class='cards-item__content'><div class='cards-item__name'>" + subjects[i].Name + ". Сборник тестов</div><div class='cards-item__desc'><div class='cards-item__desc-icon'></div><p class='cards-item__desc-text'>5 вариантов теста по " + subjects[i].QuestionCount + " вопросов</p></div></div></a></td>";

                        i++;

                    }
                    cnt += "</tr>";
                }

                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = cnt
                };
            }
        }

        public async Task<IActionResult> SearchResult(string search)
        {  
            
            string s = $"{search}%";

            List<Subject> subjects = db.Subjects.Where(p=> EF.Functions.Like(p.Name,s)).ToList();
            if (subjects.Count!=0) {
                ViewBag.subjects = subjects;
                return View("Search"); 
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public List<Subject> listFromQuery(IQueryable<Subject> query)
        {
            List<Subject> items = new List<Subject>();
            foreach (var i in query)
            {
                items.Add(i);
            }
            
            return items;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

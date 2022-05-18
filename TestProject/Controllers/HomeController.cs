using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
            if (User.IsInRole("admin"))
            {
                return View("Index", "Users");
            }
            else {
                ViewBag.subjects = db.Subjects.ToList();
                return View();
            }
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
            if (User.IsInRole("user"))
            {
                string idUser = getCurrentUserId();
            var res = db.Results.Where(t=>t.IdUser==idUser).ToList();
            List<ResultViewModel> results = new List<ResultViewModel>();
                List<ResultViewModel> final = new List<ResultViewModel>();
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
                   
                
                            string idU = getCurrentUserId();
                            string subjectAbb = "";
                            switch (subject)
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
                            DateTime date = r.Date;
                            string dd = date.ToString("g", CultureInfo.GetCultureInfo("de-DE"));
                            dd = dd.Replace(':', '.');
                            dd = dd.Replace(" ", "_");
                            String vv = variant.Substring(8);
                            DirectoryInfo di = new DirectoryInfo(_appEnvironment.WebRootPath + "/result");
                            FileInfo[] rgFile = di.GetFiles().Where(s => s.Name.Contains($"{Name}_{subjectAbb}_{vv}_{dd}.pdf")).ToArray();
                    ResultViewModel result;
                    if (rgFile.Length != 0)
                    {
                         result = new ResultViewModel { Id = r.Id, IdUser = r.IdUser, IdTest = r.IdTest, Subject = subject, Variant = variant, Mark = r.Mark, Date = r.Date, Url = rgFile[0] };
                    }
                    else
                    {
                       result = new ResultViewModel { Id = r.Id, IdUser = r.IdUser, IdTest = r.IdTest, Subject = subject, Variant = variant, Mark = r.Mark, Date = r.Date };
                    }
                        results.Add(result);

                }
                       
                    ViewBag.results = results;
                    return View("UserResult");
            }
            else
            {
                return StatusCode(403);
            }
        }

        public async Task<IActionResult> GetPage(string URL, int subject)
        {
            if (User.IsInRole("user"))
            {
                ViewBag.variants = db.Tests.Where(a => a.IdSubject == subject).ToList();
                Subject sub = db.Subjects.Where(s => s.Id == subject).First();
                return View(URL, sub);
            }
            else
            {
                return StatusCode(403);
            }
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

                        cnt += "<td><a class='cards-item text-center'  href='/Home/GetPage?url=" + subjects[i].PageUrl + "&subject=" + subjects[i].Id + "'><div class='bubble'><div class='rectangle'>"+subjects[i].Name+ "</div><div class='triangle-l'></div><div class='triangle-r'></div><div class='info'><img src =" + subjects[i].ImgURL + "><div class='cards-item__content'><div class='cards-item__desc'><p class='sub'>5 вариантов теста по " + subjects[i].QuestionCount + " вопросов</p></div></div></div></div></a></td>";

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
                String type = "";
                switch (id){
                    case "r-1":
                        type = "Технические";
                        break;
                    case "r-2":
                        type = "Гуманитарные";
                        break;
                    case "r-3":
                        type = "Естественные";
                        break;
                }
                List<Subject> subjects = db.Subjects.Where(t => t.Type == type).ToList();

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

                        cnt += "<td><a class='cards-item text-center'  href='/Home/GetPage?url=" + subjects[i].PageUrl + "&subject=" + subjects[i].Id + "'><div class='bubble'><div class='rectangle'>" + subjects[i].Name + "</div><div class='triangle-l'></div><div class='triangle-r'></div><div class='info'><img src =" + subjects[i].ImgURL + "><div class='cards-item__content'><div class='cards-item__desc'><p class='sub'>5 вариантов теста по " + subjects[i].QuestionCount + " вопросов</p></div></div></div></div></a></td>";

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

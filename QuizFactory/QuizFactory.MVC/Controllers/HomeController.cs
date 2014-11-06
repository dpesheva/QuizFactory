using QuizFactory.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizFactory.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (this.db.QuizzesDefinitions.All().Any())
            {
                var topPopular = this.db.QuizzesDefinitions.All().Take(3).Select(QuizViewModel.FromQuizDefinition).ToList();
                ViewBag.TopMostPopular = topPopular;

            }


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


    }
}
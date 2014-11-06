using QuizFactory.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        //public ActionResult RecentContent()
        //{
        //    if (!Request.IsAjaxRequest())
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.Forbidden;
        //        return null; // TODO return error
        //    }

        //    List<QuizViewModel> quizzes = this.db.QuizzesDefinitions.All().Select(QuizViewModel.FromQuizDefinition).ToList();
        //    ViewBag.TabContent = quizzes;
            
        //    return null;// TODO return smthng
        //}

        public ActionResult RecentContent()
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            List<QuizViewModel> quizzes = this.db.QuizzesDefinitions.All().Select(QuizViewModel.FromQuizDefinition).ToList();
            ViewBag.TabContent = quizzes;

            return this.PartialView("_ListQuizBoxesPartial", quizzes);
        }

       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


    }
}
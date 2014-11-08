namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using QuizFactory.Mvc.ViewModels;

    public class PlayController : BaseController
    {
        // GET: Play
        public ActionResult Index()
        {
            return View();
        }

        // GET: Play/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //GET : Play/Start
        [HttpGet]
        public ActionResult StartQuiz(int id)
        {
            var quiz = this.db.QuizzesDefinitions
                .SearchFor(q => q.Id == id)
                .Select(QuizViewModel.FromQuizDefinition)
                .FirstOrDefault();

            if (quiz == null)
            {
                return Redirect("Error"); // TODO 
            }

            return View(quiz);
        }

        // POST: Play/Start
        [HttpPost]
        public ActionResult StartQuiz(int id, int answerId)
        {
            try
            {
                // TODO: Add insert logic here

                //return next partial
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetQuestion(int quizId, int currentQuestion)
        {
            
            var question = this.db.QuestionsDefinitions
                .SearchFor(q => q.QuizDefinition.Id == quizId )
                .Select(QuestionViewModel.FromQuestionDefinition)
                .FirstOrDefault(); // TODO get next question

            if (question == null)
            {
                return Redirect("Error"); // TODO 
            }

            return this.PartialView("_OpenQuestionPartial", question);
        }

    }
}

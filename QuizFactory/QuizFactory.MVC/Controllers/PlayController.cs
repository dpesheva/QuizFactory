namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using QuizFactory.Mvc.Areas.Users.ViewModels;
    using QuizFactory.Mvc.ViewModels;

    public class PlayController : BaseController
    {
        // GET: Play
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: Play/Details/5
        public ActionResult Details(int id)
        {
            return this.View();
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
                return this.Redirect("Error"); // TODO 
            }

            return this.View(quiz);
        }

        // POST: Play/Start
        [HttpPost]
        public ActionResult StartQuiz(int id, int answerId)
        {
            try
            {
                // TODO: Add insert logic here
                //return next partial
                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        public ActionResult GetQuestion(int quizId, int currentQuestion)
        {
            var question = this.db.QuestionsDefinitions
                               .SearchFor(q => q.QuizDefinition.Id == quizId)
                               .Select(QuestionViewModel.FromQuestionDefinition)
                               .FirstOrDefault(); // TODO get next question

            if (question == null)
            {
                return this.Redirect("Error"); // TODO 
            }

            return this.PartialView("_OpenQuestionPartial", question);
        }
    }
}
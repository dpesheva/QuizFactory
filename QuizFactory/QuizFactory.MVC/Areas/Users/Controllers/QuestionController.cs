namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Controllers;
    using QuizFactory.Mvc.Filters;
    using QuizFactory.Mvc.ViewModels;

    [OwnerOrAdminAttribute("quizId")]
    public class QuestionController : BaseController
    {
        public QuestionController(IQuizFactoryData data)
            : base(data)
        {
        }

        public ActionResult Index(int? quizId)
        {
            if (quizId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.TempData["quizId"] = quizId;
            this.TempData["quizTitle"] = this.Db.QuizzesDefinitions.Find(quizId).Title;

            var allQuestions = this.Db.QuestionsDefinitions
                                   .All()
                                   .Where(q => q.QuizDefinition.Id == quizId)
                                   .Select(QuestionViewModel.FromQuestionDefinition)
                                   .ToList();

            return this.View(allQuestions);
        }

        public ActionResult Details(int? id)
        {
            var questionViewModel = this.GetById(id);

            if (questionViewModel == null)
            {
                return this.HttpNotFound();
            }

            return this.View(questionViewModel);
        }

        // GET: Users/Question/Add
        public ActionResult Add(int? quizId)
        {
            this.ViewBag.PageTitle = "Add";
            return this.View("AddEdit");
        }

        // POST: Users/Question/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(QuestionViewModel questionViewModel, int? quizId)
        {
            this.ViewBag.PageTitle = "Add";
            return this.AddOrEdit(questionViewModel, quizId, false);
        }

        // GET: Users/Question/Edit/5
        public ActionResult Edit(int? id)
        {
            var questionViewModel = this.GetById(id);

            if (questionViewModel == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.PageTitle = "Edit";
            return this.View("AddEdit", questionViewModel);
        }

        // POST: Users/Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModel questionViewModel, int? quizId)
        {
            this.ViewBag.PageTitle = "Edit";
            return this.AddOrEdit(questionViewModel, quizId, true);
        }

        // GET: Users/Question/Delete/5
        public ActionResult Delete(int? id)
        {
            var questionViewModel = this.GetById(id);

            if (questionViewModel == null)
            {
                return this.HttpNotFound();
            }
            return this.View(questionViewModel);
        }

        // POST: Users/Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var question = this.Db.QuestionsDefinitions.Find(id);
            var quizId = question.QuizDefinition.Id;

            this.Db.QuestionsDefinitions.Delete(question);
            this.Db.SaveChanges();
            return this.RedirectToAction("Index", new { quizId = quizId });
        }

        private ActionResult AddOrEdit(QuestionViewModel questionViewModel, int? quizId, bool edit)
        {
            var quiz = this.Db.QuizzesDefinitions.Find(quizId);
            if (quiz == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (this.ModelState.IsValid)
            {
                for (var i = 0; i < questionViewModel.Answers.Count; i++)
                {
                    questionViewModel.Answers[i].IsCorrect = false;
                }

                questionViewModel.Answers[questionViewModel.CorrectAnswerId].IsCorrect = true;

                if (edit)
                {
                    this.Db.QuestionsDefinitions.Delete(questionViewModel.Id);
                }

                var newQuestion = new QuestionDefinition();

                this.MapFromModel(questionViewModel, newQuestion);
                newQuestion.QuizDefinition = quiz;

                this.Db.QuestionsDefinitions.Add(newQuestion);
                this.Db.SaveChanges();
                return this.RedirectToAction("Index", new { quizId = quizId });
            }

            return this.View("AddEdit", questionViewModel);
        }

        private void MapFromModel(QuestionViewModel questionViewModel, QuestionDefinition newQuestion)
        {
            newQuestion.QuestionText = questionViewModel.QuestionText;
            for (var i = 0; i < questionViewModel.Answers.Count; i++)
            {
                var item = questionViewModel.Answers[i];

                var answ = new AnswerDefinition()
                {
                    Text = item.Text,
                    Position = i,
                    QuestionDefinition = newQuestion,
                    IsCorrect = item.IsCorrect
                };

                this.Db.AnswerDefinitions.Add(answ);
            }
        }

        private QuestionViewModel GetById(int? id)
        {
            if (id == null)
            {
                throw new HttpException("Incorrect question identifier");
            }

            var questionViewModel = this.Db.QuestionsDefinitions
                                        .All()
                                        .Where(q => q.Id == id)
                                        .Select(QuestionViewModel.FromQuestionDefinition)
                                        .FirstOrDefault();
            return questionViewModel;
        }
    }
}
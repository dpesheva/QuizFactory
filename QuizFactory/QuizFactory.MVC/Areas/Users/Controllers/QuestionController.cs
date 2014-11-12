namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Controllers;
    using QuizFactory.Mvc.Filters;
    using QuizFactory.Mvc.ViewModels;

    [OwnerOrAdminAttribute]
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

            var allQuestions = this.db.QuestionsDefinitions
                                     .All()
                                     .Where(q => q.QuizDefinition.Id == quizId)
                                     .Select(QuestionViewModel.FromQuestionDefinition)
                                     .ToList();

            return this.View(allQuestions);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuestionViewModel questionViewModel = this.db.QuestionsDefinitions
                                                       .All()
                                                       .Where(q => q.Id == id)
                                                       .Select(QuestionViewModel.FromQuestionDefinition)
                                                       .FirstOrDefault();

            if (questionViewModel == null)
            {
                return this.HttpNotFound();
            }
            return this.View(questionViewModel);
        }

        // GET: Users/Question/Add
        public ActionResult Add(int? quizId)
        {
            return this.View();
        }

        // POST: Users/Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(QuestionViewModel questionViewModel, int? quizId)
        {
            var quiz = this.db.QuizzesDefinitions.Find(quizId);
            if (quiz == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (this.ModelState.IsValid)
            {
                var newQuestion = new QuestionDefinition();

                this.MapFromModel(questionViewModel, newQuestion);
                newQuestion.QuizDefinition = quiz;

                this.db.QuestionsDefinitions.Add(newQuestion);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(questionViewModel);
        }

        // GET: Users/Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var questionViewModel = this.db.QuestionsDefinitions
                                        .All()
                                        .Where(q => q.Id == id)
                                        .Select(QuestionViewModel.FromQuestionDefinition)
                                        .FirstOrDefault();

            if (questionViewModel == null)
            {
                return this.HttpNotFound();
            }
            return this.View(questionViewModel);
        }

        // POST: Users/Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModel questionViewModel, int? quizId)
        {
            if (this.ModelState.IsValid)
            {
                // TODO create new and set quiz id
                this.db.SaveChanges();
                return this.RedirectToAction("Index", new { quizId = quizId });
            }
            return this.View(questionViewModel);
        }

        // GET: Users/Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var questionViewModel = this.db.QuestionsDefinitions
                                        .All()
                                        .Where(q => q.Id == id)
                                        .Select(QuestionViewModel.FromQuestionDefinition)
                                        .FirstOrDefault();

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
            var question = this.db.QuestionsDefinitions.Find(id);
            var quizId = question.QuizDefinition.Id;

            this.db.QuestionsDefinitions.Delete(question);
            this.db.SaveChanges();
            return this.RedirectToAction("Index", new { quizId = quizId });
        }

        private void MapFromModel(QuestionViewModel questionViewModel, QuestionDefinition newQuestion)
        {
            newQuestion.QuestionText = questionViewModel.QuestionText;
            foreach (var item in newQuestion.AnswersDefinitions)
            {
                if (item != null)
                {
                    new AnswerDefinition()
                    {
                        Text = item.Text,
                        Position = item.Position,
                        QuestionDefinition = newQuestion
                    };

                    newQuestion.AnswersDefinitions.Add(item);
                }
            }
        }
    }
}
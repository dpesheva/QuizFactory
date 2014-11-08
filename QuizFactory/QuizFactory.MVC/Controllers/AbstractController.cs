using Microsoft.AspNet.Identity;
using QuizFactory.Models;
using QuizFactory.Mvc.Areas.Users.ViewModels;
using QuizFactory.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuizFactory.Mvc.Controllers
{
    public abstract class AbstractController : BaseController
    {
        // GET: ***/QuizAdministration
        public ActionResult Index()
        {
            var allQuizzes = GetAllQuizzes();
            return this.View(allQuizzes);
        }

        // GET: ***/QuizAdministration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizViewModel = this.GetViewModelById(id);

            if (quizViewModel == null)
            {
                return this.HttpNotFound();
            }
            return this.View(quizViewModel);
        }

        // GET: ***/QuizAdministration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizViewModel quizAdminViewModel = this.GetViewModelById(id);

            if (quizAdminViewModel == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().Where(c => c.IsDeleted == false).ToList(), "Id", "Name");

            return this.View(quizAdminViewModel);
        }

        // POST: ***/QuizAdministration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuizViewModel quizViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var quiz = db.QuizzesDefinitions.Find(quizViewModel.Id);
                MapViewModelToModel(quizViewModel, quiz);

                this.db.SaveChanges();

                quizViewModel.Id = quiz.Id;

                return this.RedirectToAction("Index");
            }

            this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().Where(c => c.IsDeleted == false).ToList(), "Id", "Name", quizViewModel.CategoryId);

            return this.View(quizViewModel);
        }

        // GET: ***/QuizAdministration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizViewModel quizAdminViewModel = this.GetViewModelById(id);

            if (quizAdminViewModel == null)
            {
                return this.HttpNotFound();
            }
            return this.View(quizAdminViewModel);
        }

        // POST: ***/QuizAdministration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var quiz = this.db.QuizzesDefinitions
                           .SearchFor(q => q.Id == id)
                           .FirstOrDefault();

            quiz.IsDeleted = true;

            this.db.QuizzesDefinitions.Update(quiz);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }

        protected virtual List<QuizViewModel> GetAllQuizzes()
        {
            var allQuizzes = this.db.QuizzesDefinitions
                                 .All()
                                 .Select(QuizViewModel.FromQuizDefinition)
                                 .ToList();
            return allQuizzes;
        }

        protected virtual QuizViewModel GetViewModelById(int? id)
        {
            // TODO Refactor

            if (id == null)
            {
                // TODO
            }
            QuizViewModel quizAdminViewModel = this.db.QuizzesDefinitions
                                                        .SearchFor(q => q.Id == id)
                                                        .Select(QuizViewModel.FromQuizDefinition)
                                                        .FirstOrDefault();

            return quizAdminViewModel;
        }

        protected virtual void MapViewModelToModel(QuizViewModel quizViewModel, QuizDefinition quiz)
        {
            var category = this.db.Categories.SearchFor(c => c.Id == quizViewModel.CategoryId).FirstOrDefault();
            if (category == null)
            {
                return; // TODO
            }

            quiz.Title = quizViewModel.Title;
            quiz.CategoryId = quizViewModel.CategoryId;
            quiz.IsPublic = quizViewModel.IsPublic;

        }
    }
}
namespace QuizFactory.Mvc.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Areas.Admin.ViewModels;
    using System.Collections.Generic;
    using QuizFactory.Mvc.Controllers;
    using QuizFactory.Mvc.Areas.Users.ViewModels;

    [Authorize(Roles = "admin")]
    public class QuizAdministrationController : AbstractController
    {
        // GET: Admin/QuizAdministration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizAdminViewModel quizAdminViewModel = this.GetViewModelById(id);

            if (quizAdminViewModel == null)
            {
                return this.HttpNotFound();
            }
            return this.View(quizAdminViewModel);
        }

        // GET: Admin/QuizAdministration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizAdminViewModel quizAdminViewModel = this.GetViewModelById(id);

            if (quizAdminViewModel == null)
            {
                return this.HttpNotFound();
            }

            this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().ToList(), "Id", "Name", quizAdminViewModel.CategoryId);

            return this.View(quizAdminViewModel);
        }

        // POST: Admin/QuizAdministration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuizAdminViewModel quizAdminViewModel)
        {
            if (this.ModelState.IsValid)
            {
                // TODO create new and disable the old
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().ToList(), "Id", "Name", quizAdminViewModel.CategoryId);

            return this.View(quizAdminViewModel);
        }

        // GET: Admin/QuizAdministration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizAdminViewModel quizAdminViewModel = this.GetViewModelById(id);

            if (quizAdminViewModel == null)
            {
                return this.HttpNotFound();
            }
            return this.View(quizAdminViewModel);
        }

        private QuizAdminViewModel GetViewModelById(int? id)
        {
            QuizAdminViewModel quizAdminViewModel = this.db.QuizzesDefinitions
                                                        .SearchFor(q => q.Id == id)
                                                        .Select(QuizAdminViewModel.FromQuizDefinition)
                                                        .FirstOrDefault();
            return quizAdminViewModel;
        }

        protected override void MapViewModelToModel(IQuizViewModel quizViewModel, QuizDefinition quiz)
        {
            base.MapViewModelToModel(quizViewModel, quiz);

            var user = this.db.Users.SearchFor(u => u.UserName == quizViewModel.Author).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException("No such username");
            }

            quiz.Author = user;
            quiz.Rating = quizViewModel.Rating;
        }
    }
}
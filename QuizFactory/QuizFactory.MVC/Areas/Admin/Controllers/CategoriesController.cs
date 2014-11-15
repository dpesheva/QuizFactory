namespace QuizFactory.Mvc.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Areas.Admin.ViewModels;
    using QuizFactory.Mvc.Controllers;

    [Authorize(Roles = "admin")]
    public class CategoriesController : BaseController
    {
        public CategoriesController(IQuizFactoryData data)
            : base(data)
        {
        }

        // GET: Admin/Categories
        public ActionResult Index()
        {
            var allCategories = this.db.Categories.All().Project().To<CategoryViewModel>().ToList();

            return this.View(allCategories);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")]
                                   CategoryViewModel category)
        {
            if (this.db.Categories.SearchFor(c => c.Name == category.Name && c.IsDeleted == false).FirstOrDefault() != null)
            {
                this.ModelState.AddModelError("Name", "There is a category with the same name!");
            }

            if (this.ModelState.IsValid)
            {
                Category newCategory = Mapper.Map<Category>(category);

                this.db.Categories.Add(newCategory);
                this.db.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryViewModel category = this.db.Categories.SearchFor(c => c.Id == id).Project().To<CategoryViewModel>().FirstOrDefault();

            if (category == null)
            {
                return this.HttpNotFound();
            }
            return this.View(category);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel category)
        {
            if (this.db.Categories.SearchFor(c => c.Name == category.Name && c.Id != category.Id && c.IsDeleted == false).FirstOrDefault() != null)
            {
                this.ModelState.AddModelError("Name", "There is a category with the same name!");
            }

            if (this.ModelState.IsValid)
            {
                Category categoryToUpdate = this.db.Categories.Find(category.Id);
                categoryToUpdate.Name = category.Name;

                this.db.SaveChanges();

                return this.RedirectToAction("Index");
            }
            return this.View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryViewModel category = this.db.Categories.SearchFor(c => c.Id == id).Project().To<CategoryViewModel>().FirstOrDefault();

            if (category == null)
            {
                return this.HttpNotFound();
            }
            return this.View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (this.db.QuizzesDefinitions.All().Any(q => q.CategoryId == id))
            {
                this.ModelState.AddModelError("Error", "Category can't be deleted. There are quzzes linked to it.");
                CategoryViewModel categoryModel = this.db.Categories.SearchFor(c => c.Id == id).Project().To<CategoryViewModel>().FirstOrDefault();
                return this.View(categoryModel);
            }
            Category category = this.db.Categories.Find(id);

            this.db.Categories.Delete(category);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }
    }
}
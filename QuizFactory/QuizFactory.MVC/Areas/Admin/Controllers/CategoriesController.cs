namespace QuizFactory.Mvc.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using QuizFactory.Models;

    public class CategoriesController : AdminController
    {
        // GET: Admin/Categories
        public ActionResult Index()
        {
            return this.View(this.db.Categories.All().Where(c => c.IsDeleted == false).ToList());
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = this.db.Categories.Find(id);
            if (category == null)
            {
                return this.HttpNotFound();
            }
            return this.View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Category category)
        {
            if (this.db.Categories.SearchFor(c => c.Name == category.Name).FirstOrDefault() != null)
            {
                this.ModelState.AddModelError("Name", "There is a category with the same name!");
            }

            if (this.ModelState.IsValid)
            {
                this.db.Categories.Add(category);
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
            Category category = this.db.Categories.Find(id);
            if (category == null)
            {
                return this.HttpNotFound();
            }
            return this.View(category);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (this.db.Categories.SearchFor(c => c.Name == category.Name && c.Id != category.Id).FirstOrDefault() != null)
            {
                this.ModelState.AddModelError("Name", "There is a category with the same name!");
            }

            if (this.ModelState.IsValid)
            {
                this.db.Categories.Update(category);
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
            Category category = this.db.Categories.Find(id);
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
            Category category = this.db.Categories.Find(id);
            category.IsDeleted = true;

            this.db.Categories.Update(category);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }


        // TODO ??
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using QuizFactory.Data;
    using QuizFactory.Models;
    using QuizFactory.Mvc.Controllers;

    // TODO DElete
    public class QuizDefinitionsController : AbstractController
    {
        QuizFactoryDbContext db = new QuizFactoryDbContext();

        // GET: Users/QuizDefinitions
        public ActionResult Index()
        {
            var quizDefinitions = this.db.QuizDefinitions.Include(q => q.Category);
            return this.View(quizDefinitions.ToList());
        }

        // GET: Users/QuizDefinitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizDefinition quizDefinition = this.db.QuizDefinitions.Find(id);
            if (quizDefinition == null)
            {
                return this.HttpNotFound();
            }
            return this.View(quizDefinition);
        }

        // GET: Users/QuizDefinitions/Create
        public ActionResult Create()
        {
            this.ViewBag.CategoryId = new SelectList(this.db.Categories, "Id", "Name");
            return this.View();
        }

        // POST: Users/QuizDefinitions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,CreatedOn,Rating,CategoryId,IsPublic,IsDeleted,UpdatedOn")]
                                   QuizDefinition quizDefinition)
        {
            if (this.ModelState.IsValid)
            {
                this.db.QuizDefinitions.Add(quizDefinition);
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            this.ViewBag.CategoryId = new SelectList(this.db.Categories, "Id", "Name", quizDefinition.CategoryId);
            return this.View(quizDefinition);
        }

        // GET: Users/QuizDefinitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizDefinition quizDefinition = this.db.QuizDefinitions.Find(id);
            if (quizDefinition == null)
            {
                return this.HttpNotFound();
            }
            this.ViewBag.CategoryId = new SelectList(this.db.Categories, "Id", "Name", quizDefinition.CategoryId);
            return this.View(quizDefinition);
        }

        // POST: Users/QuizDefinitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,CreatedOn,Rating,CategoryId,IsPublic,IsDeleted,UpdatedOn")] QuizDefinition quizDefinition)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Entry(quizDefinition).State = EntityState.Modified;
                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }
            this.ViewBag.CategoryId = new SelectList(this.db.Categories, "Id", "Name", quizDefinition.CategoryId);
            return this.View(quizDefinition);
        }

        // GET: Users/QuizDefinitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizDefinition quizDefinition = this.db.QuizDefinitions.Find(id);
            if (quizDefinition == null)
            {
                return this.HttpNotFound();
            }
            return this.View(quizDefinition);
        }

        // POST: Users/QuizDefinitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizDefinition quizDefinition = this.db.QuizDefinitions.Find(id);
            this.db.QuizDefinitions.Remove(quizDefinition);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
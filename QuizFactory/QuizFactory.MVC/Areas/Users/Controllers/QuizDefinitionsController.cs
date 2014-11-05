using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuizFactory.Data;
using QuizFactory.Models;

namespace QuizFactory.Mvc.Areas.Users
{
    public class QuizDefinitionsController : Controller
    {
        private QuizFactoryDbContext db = new QuizFactoryDbContext();

        // GET: Users/QuizDefinitions
        public ActionResult Index()
        {
            var quizDefinitions = db.QuizDefinitions.Include(q => q.Category);
            return View(quizDefinitions.ToList());
        }

        // GET: Users/QuizDefinitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizDefinition quizDefinition = db.QuizDefinitions.Find(id);
            if (quizDefinition == null)
            {
                return HttpNotFound();
            }
            return View(quizDefinition);
        }

        // GET: Users/QuizDefinitions/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Users/QuizDefinitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,CreatedOn,Rating,CategoryId,IsPublic,IsActive,UpdatedOn")] QuizDefinition quizDefinition)
        {
            if (ModelState.IsValid)
            {
                db.QuizDefinitions.Add(quizDefinition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", quizDefinition.CategoryId);
            return View(quizDefinition);
        }

        // GET: Users/QuizDefinitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizDefinition quizDefinition = db.QuizDefinitions.Find(id);
            if (quizDefinition == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", quizDefinition.CategoryId);
            return View(quizDefinition);
        }

        // POST: Users/QuizDefinitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,CreatedOn,Rating,CategoryId,IsPublic,IsActive,UpdatedOn")] QuizDefinition quizDefinition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizDefinition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", quizDefinition.CategoryId);
            return View(quizDefinition);
        }

        // GET: Users/QuizDefinitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizDefinition quizDefinition = db.QuizDefinitions.Find(id);
            if (quizDefinition == null)
            {
                return HttpNotFound();
            }
            return View(quizDefinition);
        }

        // POST: Users/QuizDefinitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizDefinition quizDefinition = db.QuizDefinitions.Find(id);
            db.QuizDefinitions.Remove(quizDefinition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

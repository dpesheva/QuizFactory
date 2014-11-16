namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using QuizFactory.Data;

    [Authorize]
    public class VoteController : BaseController
    {
        public VoteController(IQuizFactoryData data)
            : base(data)
        {
        }

        [HttpPost]
        public ActionResult Vote(int value, int id)
        {
            if (value < 1 || 5 < value)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();

            Vote vote = new Vote
            {
                UserId = userId,
                QuizDefinitionId = id,
                Value = value
            };

            try
            {
                this.db.Votes.Add(vote);
                ViewBag.Voted = true;
                this.db.SaveChanges();
                UpdateQuizRating(id, value);
            }
            catch
            {
                throw new HttpException("Already voted");
            }

            return this.View();
        }

        private void UpdateQuizRating(int id, int value)
        {
            var quiz = this.db.QuizzesDefinitions.Find(id);
            var avrg = quiz.Votes.Average(v => v.Value);
            quiz.Rating = (decimal)avrg;
            this.db.SaveChanges();
        }
    }
}
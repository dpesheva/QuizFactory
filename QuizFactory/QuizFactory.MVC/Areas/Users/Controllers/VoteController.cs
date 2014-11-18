namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Controllers;

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

            var userId = this.User.Identity.GetUserId();

            Vote vote = new Vote
            {
                UserId = userId,
                QuizDefinitionId = id,
                Value = value
            };

            try
            {
                this.Db.Votes.Add(vote);
                this.ViewBag.Voted = true;
                this.Db.SaveChanges();
                this.UpdateQuizRating(id, value);
            }
            catch
            {
                throw new HttpException("Already voted");
            }

            return this.View();
        }

        private void UpdateQuizRating(int id, int value)
        {
            var quiz = this.Db.QuizzesDefinitions.Find(id);
            var avrg = quiz.Votes.Average(v => v.Value);
            quiz.Rating = (decimal)avrg;
            this.Db.SaveChanges();
        }
    }
}
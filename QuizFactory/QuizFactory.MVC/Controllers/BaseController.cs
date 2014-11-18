namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using QuizFactory.Data;

    public abstract class BaseController : Controller
    {
        private IQuizFactoryData db;

        public BaseController(IQuizFactoryData data)
        {
            this.db = data;
        }

        public IQuizFactoryData Db
        {
            get
            {
                return this.db;
            }
        }
    }
}
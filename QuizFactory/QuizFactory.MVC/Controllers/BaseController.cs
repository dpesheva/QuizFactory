using System;
using System.Collections.Generic;
using System.Linq;
namespace QuizFactory.Mvc.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;

    public abstract class BaseController : Controller
    {
        public IQuizFactoryData db;

        public BaseController(IQuizFactoryData data)
        {
            this.db = data;
        }

        public BaseController()
            : this(new QuizFactoryData())
        {
        }
    }
}
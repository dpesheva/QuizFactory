using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuizFactory.Mvc.Startup))]
namespace QuizFactory.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

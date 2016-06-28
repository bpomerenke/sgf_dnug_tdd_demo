using System.Web.Http;

namespace TDDDemoApp.Controllers
{
    public class ApplicationController : ApiController
    {
        [HttpGet]
        [Route("api/v1/info")]
        public InfoView Info()
        {
            return new InfoView {Title = "TDD Demo"};
        }
    }

    public class InfoView
    {
        public string Title { get; set; }
    }
}
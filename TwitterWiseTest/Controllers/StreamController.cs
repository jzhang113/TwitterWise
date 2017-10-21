using Microsoft.AspNetCore.Mvc;
using TwitterWise.Models;

namespace TwitterWise.Controllers
{
    public class StreamController : Controller
    {
        [Route("api/[controller]/start")]
        [HttpGet]
        public void Start()
        {
            StreamModel.Start();
        }

        [Route("api/[controller]/start/{item}")]
        [HttpGet]
        public void Start(string item)
        {
            StreamModel.AddWatch(item);
        }

        [Route("api/[controller]/stop")]
        [HttpGet]
        public void Stop()
        {
            StreamModel.StopAll();
        }

        [Route("api/[controller]/stop/{item}")]
        [HttpGet]
        public void Stop(string item)
        {
            StreamModel.RemoveWatch(item);
        }
    }
}

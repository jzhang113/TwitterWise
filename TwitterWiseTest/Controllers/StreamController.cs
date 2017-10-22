using Microsoft.AspNetCore.Mvc;
using System.Net;
using TwitterWise.Models;
using CircularBuffer;
using System.Collections.Generic;

namespace TwitterWise.Controllers
{
    public class StreamController : Controller
    {
        [Route("api/[controller]")]
        [Produces("application/json")]
        [HttpGet]
        public TweetModel Get()
        {
            TweetModel model = StreamModel.GetTweet();

            if (model == null)
            {
                return new TweetModel()
                {
                    Status = false
                };
            }
            else
            {
                model.Status = true;
                return model;
            }
        }

        [Route("api/[controller]/filtered")]
        [Produces("application/json")]
        [HttpGet]
        public TweetModel GetFiltered()
        {
            TweetModel model =  StreamModel.GetFilteredTweet();

            if (model == null)
            {
                return new TweetModel()
                {
                    Status = false
                };
            }
            else
            {
                model.Status = true;
                return model;
            }
        }

        [Route("api/[controller]/all")]
        [Produces("application/json")]
        [HttpGet]
        public IEnumerable<TweetModel> GetAll()
        {
            CircularBuffer<TweetModel> list = StreamModel.GetAllTweets();
            ICollection<TweetModel> response = new List<TweetModel>();

            foreach (TweetModel m in list)
            {
                if (m != null)
                {
                    m.Status = true;
                    response.Add(m);
                }
            }

            return response;
        }

        [Route("api/[controller]/filtered/all")]
        [Produces("application/json")]
        [HttpGet]
        public IEnumerable<TweetModel> GetAllFiltered()
        {
            CircularBuffer<TweetModel> list = StreamModel.GetAllFilteredTweets();
            ICollection<TweetModel> response = new List<TweetModel>();

            foreach (TweetModel m in list)
            {
                if (m != null)
                {
                    m.Status = true;
                    response.Add(m);
                }
            }

            return response;
        }

        [Route("api/[controller]/start")]
        [HttpGet]
        public StatusCodeResult Start()
        {
            StreamModel.Start();

            return new StatusCodeResult((int)HttpStatusCode.Accepted);
        }

        [Route("api/[controller]/start/{item}")]
        [HttpGet]
        public StatusCodeResult Start(string item)
        {
            StreamModel.AddWatch(item);

            return new StatusCodeResult((int)HttpStatusCode.Accepted);
        }

        [Route("api/[controller]/stop")]
        [HttpGet]
        public StatusCodeResult Stop()
        {
            StreamModel.StopAll();

            return new StatusCodeResult((int)HttpStatusCode.Accepted);
        }

        [Route("api/[controller]/stop/{item}")]
        [HttpGet]
        public StatusCodeResult Stop(string item)
        {
            StreamModel.RemoveWatch(item);

            return new StatusCodeResult((int)HttpStatusCode.Accepted);
        }
    }
}

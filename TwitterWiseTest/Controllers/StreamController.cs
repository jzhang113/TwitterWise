﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using TwitterWise.Models;
using CircularBuffer;

namespace TwitterWise.Controllers
{
    public class StreamController : Controller
    {
        [Route("api/[controller]")]
        [Produces("application/json")]
        [HttpGet]
        public TweetModel Get()
        {
            return StreamModel.GetTweet();
        }

        [Route("api/[controller]/all")]
        [Produces("application/json")]
        [HttpGet]
        public CircularBuffer<TweetModel> GetAll()
        {
            return StreamModel.GetAllTweets();
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

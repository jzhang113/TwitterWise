using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TwitterWiseTest.Models;

namespace TwitterWiseTest.Controllers
{
    public class SearchController : Controller
    {
        // GET api/values
        [Route("api/[controller]/{query}")]
        [HttpGet]
        public IEnumerable<TweetModel> Get(string query)
        {
            return SearchModel.Search(query);
        }
    }
}
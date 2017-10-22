using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TwitterWise.Models;

namespace TwitterWise.Controllers
{
    [Produces("application/json")]
    public class SearchController : Controller
    {
        // GET api/values
        [Route("api/[controller]/{query}")]
        [HttpGet]
        public IEnumerable<TweetModel> Get(string query)
        {
            ICollection<TweetModel> model = SearchModel.Search(query)[0];

            while (model.Count == 0)
            {
                model = SearchModel.SearchMore()[0];
            }

            return model;
        }

        [Route("api/[controller]/filtered/{query}")]
        public IEnumerable<TweetModel> GetFiltered(string query)
        {
            ICollection<TweetModel> model = SearchModel.Search(query)[1];

            while (model.Count == 0)
            {
                model = SearchModel.SearchMore()[1];
            }

            return model;
        }
    }
}
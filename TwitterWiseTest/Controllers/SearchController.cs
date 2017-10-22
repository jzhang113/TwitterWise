using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TwitterWise.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace TwitterWise.Controllers
{
    [Produces("application/json")]
    public class SearchController : Controller
    {
        // GET api/values
        [Route("api/[controller]/{query}")]
        [HttpGet]
        public JsonResult Get(string query)
        {
            ICollection<TweetModel> model = SearchModel.Search(query)[0];

            while (model.Count == 0)
            {
                var m = SearchModel.SearchMore();

                if (m != null)
                {
                    model = m[0];
                }
                else
                {
                    return null;
                }
            }

            return Json(model);
        }

        [Route("api/[controller]/filtered/{query}")]
        public IEnumerable<TweetModel> GetFiltered(string query)
        {
            ICollection<TweetModel> model = SearchModel.Search(query)[1];

            while (model.Count == 0)
            {
                var m = SearchModel.SearchMore();

                if (m != null)
                {
                    model = m[1];
                }
                else
                {
                    return null;
                }
            }

            return model;
        }
    }
}
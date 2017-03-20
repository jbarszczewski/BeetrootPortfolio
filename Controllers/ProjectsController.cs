using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BeetrootPortfolio.Models;
using System;

namespace BeetrootPortfolio.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<ProjectArticle> Get()
        {
            return new ProjectArticle[] { new ProjectArticle
            {
                Id = 1,
                CreatedOn = DateTime.Now,
                Title = "Testowy 1",
                HtmlContent = "<div><p>Hello!</p></div>"
            },
            new ProjectArticle
            {
                Id = 3,
                CreatedOn = DateTime.Now.AddDays(4).AddHours(5).AddMinutes(43),
                Title = "Testowy 3",
                HtmlContent = "<div><p>Hello again!</p></div>"
            }
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ProjectArticle Get(int id)
        {
            return new ProjectArticle
            {
                Id = 1,
                CreatedOn = DateTime.Now,
                Title = "Testowy 1",
                HtmlContent = "<div><p>Hello again!</p></div>"
            };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}

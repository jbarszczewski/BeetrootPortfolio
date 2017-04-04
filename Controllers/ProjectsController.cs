using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BeetrootPortfolio.Models;
using BeetrootPortfolio.Data;

namespace BeetrootPortfolio.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private IProjectsRepository projectsRepository;

        public ProjectsController(IProjectsRepository reporitory)
        {
            this.projectsRepository = reporitory;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            var projects = this.projectsRepository.GetProjectsAsync(p => true).Result;          
            return projects;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Project Get(string id)
        {
            return this.projectsRepository.GetProjectAsync(id).Result;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}

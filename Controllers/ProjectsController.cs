using Microsoft.AspNetCore.Mvc;
using BeetrootPortfolio.Models;
using BeetrootPortfolio.Data;
using Microsoft.Extensions.Options;
using BeetrootPortfolio.Configuration;

namespace BeetrootPortfolio.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private IProjectsRepository projectsRepository;
        private PortfolioSettings settings;

        public ProjectsController(IProjectsRepository reporitory, IOptions<PortfolioSettings> settings)
        {
            this.projectsRepository = reporitory;
            this.settings = settings.Value;
        }

        [HttpGet("info/{key}")]
        public IActionResult GetInfo(string key)
        {
            var info = this.projectsRepository.GetInfoAsync(key).Result;
            if (info == null)
                return NotFound();
            return Ok(info);
        }

        // GET: api
        [HttpGet]
        public IActionResult Get()
        {
            var projects = this.projectsRepository.GetProjectsAsync(p => true).Result;
            return Ok(projects);
        }

        // GET api/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(this.projectsRepository.GetProjectAsync(id).Result);
        }

        // POST api
        [HttpPost]
        public IActionResult Post([FromBody]Project project)
        {
            if (!Request.Headers.ContainsKey("apiKey"))
                return BadRequest("Missing 'apiKey' header.");
            var apiKey = Request.Headers["apiKey"];
            if (apiKey == this.settings.ApiKey)
                return Ok(this.projectsRepository.CreateProjectAsync(project).Result);
            return Unauthorized();
        }
    }
}

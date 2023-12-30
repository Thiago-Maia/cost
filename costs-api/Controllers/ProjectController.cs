using costs.Domain.Model;
using costs.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace costs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectRepository _projectRepository;
        private readonly ILogger<ProjectController> _logger;


        public ProjectController(ProjectRepository projectRepository, ILogger<ProjectController> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
            
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            var projects = _projectRepository.GetProjects();
            return Ok(projects);
        }


        [HttpGet("{projectId}")]
        public IActionResult GetProject(string projectId)
        {
            var projects = _projectRepository.GetProjectById(projectId);
            return Ok(projects);
        }

        [HttpPost("create")]
        public IActionResult CreateProject([FromBody] Project project)
        {
            if (string.IsNullOrEmpty(project.Name))
            {
                var mensagem = "O projeto deve ter um nome!";

                return BadRequest(new {message = mensagem});       
            }

            project.Cost = 0;
            project.Services = new List<Service>();

            _projectRepository.AddProject(project);

            return Ok(project);
        }

        [HttpPut("update")]
        public IActionResult UpdateProject([FromBody] Project project)
        {
            if(project.Services != null && project.Services.Count > 0)
            {
                var lastProject = project.Services.Last();
                lastProject.Id = new BsonObjectId(ObjectId.GenerateNewId()).ToString();
            }
                
            _projectRepository.UpdateProject(project);

            return Ok(project);
        }

        [HttpDelete("delete/{projectId}")]
        public IActionResult DeleteProject(string projectId)
        {
            _projectRepository.DeleteProject(projectId);

            return Ok();
        }

        [HttpPost("service/add")]
        public IActionResult AddService([FromBody] Service service, string projectId)
        {
            var project = _projectRepository.GetProjectById(projectId);

            service.Id = new BsonObjectId(ObjectId.GenerateNewId()).ToString();
            project.Services.Add(service);

            project.Cost += service.Cost;
            
            _projectRepository.UpdateProject(project);

            return Ok(project);
        }

        [HttpDelete("service/remove")]
        public IActionResult DeleteProject(string projectId, string serviceId)
        {
            var project = _projectRepository.GetProjectById(projectId);

            var service = project.Services.FirstOrDefault(s => s.Id == serviceId);

            if(service == null)
                return NotFound("Serviço não encontrado!");

            project.Cost -= service.Cost;

            project.Services.Remove(service);

            _projectRepository.UpdateProject(project);

            return Ok(project);
        }
    }
}

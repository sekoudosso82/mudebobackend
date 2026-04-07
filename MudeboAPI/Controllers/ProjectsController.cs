using BusinessLayer;
using DbLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLayer;

namespace MudeboAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        // properties
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjects _project;
        private readonly MudeboDb _mudeboDb;

        // constructor
        public ProjectsController(ILogger<ProjectsController> logger,
                                  IProjects project, 
                                  MudeboDb mudeboDb)
        {
            _logger = logger;
            _project = project;
            _mudeboDb = mudeboDb;
        }

        // Methods
        [AllowAnonymous]
        [HttpGet]// GET all Projects
        public async Task<IEnumerable<Projects>> ProjectsList()
        {
            List<Projects> projects = await _project.ProjectsListAsync();
            return projects;
        }

        [AllowAnonymous]
        [HttpGet("{id}")] // GET api/<ProjectsController>/5
        public async Task<Projects> GetProject(int projectId)
        {
            List<Projects> projectsList = await _project.ProjectsListAsync();
            var returnedproject = projectsList.Where(x=>x.ProjectId==projectId).FirstOrDefault();
            return returnedproject;
        }

        [AllowAnonymous]
        [HttpPost("CreateNewProject")] // create new Project
        [Authorize(Roles = "Admin")]
        public async Task<bool> CreateNewProjectAsync(Projects p)
        {
            await _mudeboDb.Projects.AddAsync(p);
            try
            { await _mudeboDb.SaveChangesAsync(); }
            catch(DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            return true;
        }

        [HttpPut("{activityId}")] // PUT api/<ProjectsController>/5
        [Authorize(Roles = "Admin")]
        public async Task<bool> EditProjectAsync(int projectId, Projects projectUpdated)
        {
            try
            {
                var p = await _mudeboDb.Projects.FindAsync(projectId);
                if(p != null)
                { await _project.EditProjectAsync(p, projectUpdated);} 
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            return true;
        }

        [HttpDelete("{id}")] // DELETE api/<ProjectsController>/5
        [Authorize(Roles = "Admin")]
        public async Task<bool> DeleteActivity(int projectId)
        {
            try
            {
                var p = await _mudeboDb.Projects.FindAsync(projectId);
                if (p != null)
                { 
                    _mudeboDb.Remove(p);
                    await _mudeboDb.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            return true;
        }
    } 
}

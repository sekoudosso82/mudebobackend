using BusinessLayer;
using DbLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLayer;
using System.Diagnostics;

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
        private readonly IWebHostEnvironment _environment;


        // constructor
        public ProjectsController(ILogger<ProjectsController> logger,
                                  IProjects project, 
                                  MudeboDb mudeboDb,
                                  IWebHostEnvironment environment)
        {
            _logger = logger;
            _project = project;
            _mudeboDb = mudeboDb;
            _environment = environment;
            
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


        // create new project
        [AllowAnonymous]
        [HttpPost("CreateNewProject")] // create new Project
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewProject ([FromForm] ProjectDto dto)
        {
            var ProjectPhotoPath = " ";

            if (dto.ProjectPhotoUrl != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "project-photo");
                if (!Directory.Exists(uploadsFolder)) { Directory.CreateDirectory(uploadsFolder); }


                var extension = Path.GetExtension(dto.ProjectPhotoUrl.FileName);
                //string fileName = Guid.NewGuid().ToString() + extension;
                string fileName = Guid.NewGuid().ToString() + dto.ProjectPhotoUrl.FileName;

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                { await dto.ProjectPhotoUrl.CopyToAsync(stream); }
                var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";
                ProjectPhotoPath = $"{Request.Scheme}://{Request.Host}/project-photo/{fileName}";
            }

            var newProject = new Projects
            {
                ProjectTitle = dto.ProjectTitle,
                ProjectDescription = dto.ProjectDescription,
                ProjectDate = DateTime.UtcNow,
                ProjectStatus = dto.ProjectStatus,
                ProjectPhotoUrl = ProjectPhotoPath,
                // ActivityPhotoUrl = dto.ActivityPhotoUrl?.FileName,
            };
            _mudeboDb.Projects.Add(newProject);
            await _mudeboDb.SaveChangesAsync();
            return Ok(newProject.ProjectId);

        }

        [HttpPut("{projectId}")] // PUT api/<ProjectsController>/5
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> EditProjectAsync(int projectId, [FromForm] ProjectDto dto)
        {
            var ProjectPhotoPath = " ";

            if (dto.ProjectPhotoUrl != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "project-photo");
                if (!Directory.Exists(uploadsFolder)) { Directory.CreateDirectory(uploadsFolder); }


                var extension = Path.GetExtension(dto.ProjectPhotoUrl.FileName);
                //string fileName = Guid.NewGuid().ToString() + extension;
                string fileName = Guid.NewGuid().ToString() + dto.ProjectPhotoUrl.FileName;

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                { await dto.ProjectPhotoUrl.CopyToAsync(stream); }
                var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";
                ProjectPhotoPath = $"{Request.Scheme}://{Request.Host}/project-photo/{fileName}";
            }

            var updatedProject = new Projects
            {
                ProjectTitle = dto.ProjectTitle,
                ProjectDescription = dto.ProjectDescription,
                ProjectDate = DateTime.UtcNow,
                ProjectStatus = dto.ProjectStatus,
                ProjectPhotoUrl = ProjectPhotoPath,
                // ActivityPhotoUrl = dto.ActivityPhotoUrl?.FileName,
            };
            try
            {
                var p = await _mudeboDb.Projects.FindAsync(projectId);
                if(p != null)
                { await _project.EditProjectAsync(projectId, updatedProject);} 
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
        [AllowAnonymous]
        // [Authorize(Roles = "Admin")]
        public async Task<bool> DeleteProject(int projectId)
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

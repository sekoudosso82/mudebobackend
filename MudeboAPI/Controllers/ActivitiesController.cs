using BusinessLayer;
using DbLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLayer;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace MudeboAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        // properties
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ActivitiesController> _logger;
        private readonly IActivities _activity;
        private readonly MudeboDb _mudeboDb;

        // constructor to inject in business layer (ActivitiesService)
        public ActivitiesController(ILogger<ActivitiesController> logger,
                                    IActivities activity, MudeboDb mudeboDb,
                                    IWebHostEnvironment environment)
        {
            _logger = logger;
            _activity = activity;
            _mudeboDb = mudeboDb;
            _environment = environment;
        }

        // Methods
       
        [AllowAnonymous]
        [HttpGet]// GET all Activities
        public async Task<IEnumerable<Activities>> ActivitiesList()
        {
            List<Activities> Activities = await _activity.ActivitiesListAsync();
            return Activities;
        }

        // GET api/<ActivitiesController>/5
        [AllowAnonymous]
        [HttpGet("{id}")] // public string get(int id)
        public async Task<Activities> GetActivity(int activityId)
        {
            List<Activities> ActivitiesList = await _activity.ActivitiesListAsync();
            var returnedactivity = ActivitiesList.Where(x=>x.ActivityId==activityId).FirstOrDefault();
            return returnedactivity;
        }

        // create new member
        [AllowAnonymous]
        [HttpPost("CreateNewActivity")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewActivity([FromForm] ActivitiesDto dto)
                 
        {
            string? photodUrl = null;
            var ActivityPhotoPath = " ";

            if (dto.ActivityPhotoUrl != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "activity-photo");
                if (!Directory.Exists(uploadsFolder)) { Directory.CreateDirectory(uploadsFolder); }


                var extension = Path.GetExtension(dto.ActivityPhotoUrl.FileName);
                //string fileName = Guid.NewGuid().ToString() + extension;
                string fileName = Guid.NewGuid().ToString() + dto.ActivityPhotoUrl.FileName;

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                { await dto.ActivityPhotoUrl.CopyToAsync(stream); }
                var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";
                ActivityPhotoPath = $"{Request.Scheme}://{Request.Host}/activity-photo/{fileName}";
                //ActivityPhotoPath = filePath;

            }
            var activity = new Activities
            {
                //ActivityId = dto.ActivityId,
                ActivityTitle = dto.ActivityTitle,
                ActivityDescription = dto.ActivityDescription,
                ActivityDate = DateTime.UtcNow,
                ActivityStatus = dto.ActivityStatus,
                ActivityPhotoUrl = ActivityPhotoPath,
                // ActivityPhotoUrl = dto.ActivityPhotoUrl?.FileName,
            };
            _mudeboDb.Activities.Add(activity);
            await _mudeboDb.SaveChangesAsync();
            return Ok(activity.ActivityId);
        }

        [HttpPut("{activityId}")] // PUT api/<ActivitiesController>/5
        [AllowAnonymous]
        // [Authorize(Roles = "Admin")]
        public async Task<bool> EditActivityAsync(int activityId, [FromForm] ActivitiesDto dto)
        {
            var ActivityPhotoPath = " ";

            if (dto.ActivityPhotoUrl != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "activity-photo");
                if (!Directory.Exists(uploadsFolder)) { Directory.CreateDirectory(uploadsFolder); }


                var extension = Path.GetExtension(dto.ActivityPhotoUrl.FileName);
                //string fileName = Guid.NewGuid().ToString() + extension;
                string fileName = Guid.NewGuid().ToString() + dto.ActivityPhotoUrl.FileName;

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                { await dto.ActivityPhotoUrl.CopyToAsync(stream); }
                var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";
                ActivityPhotoPath = $"{Request.Scheme}://{Request.Host}/activity-photo/{fileName}";
            }
            var activity = new Activities
            {
                //ActivityId = dto.ActivityId,
                ActivityTitle = dto.ActivityTitle,
                ActivityDescription = dto.ActivityDescription,
                ActivityDate = DateTime.UtcNow,
                ActivityStatus = dto.ActivityStatus,
                ActivityPhotoUrl = ActivityPhotoPath,
                        // ActivityPhotoUrl = dto.ActivityPhotoUrl?.FileName,
            };
            try
                {
                var act = await _mudeboDb.Activities.FindAsync(activityId);
                if(act != null)
                {
                    await _activity.EditActivitiesAsync(activityId, activity);
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
            //return Ok(activity.ActivityId);

        }

        [AllowAnonymous]
        [HttpDelete("{id}")] // DELETE api/<ActivitiesController>/5
        //[Authorize(Roles = "Admin")]
        public async Task<bool> DeleteActivityAsync(int ActivityId)
        {
            try
            {
                var act = await _mudeboDb.Activities.FindAsync(ActivityId);
                if (act != null)
                { 
                    _mudeboDb.Remove(act);
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

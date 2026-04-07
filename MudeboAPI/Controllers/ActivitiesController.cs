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
    public class ActivitiesController : ControllerBase
    {
        // properties
        private readonly ILogger<ActivitiesController> _logger;
        private readonly IActivities _activity;
        private readonly MudeboDb _mudeboDb;

        // constructor to inject in business layer (ActivitiesService)
        public ActivitiesController(ILogger<ActivitiesController> logger,
                                    IActivities activity, MudeboDb mudeboDb)
        {
            _logger = logger;
            _activity = activity;
            _mudeboDb = mudeboDb;
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
        [Authorize(Roles = "Admin")]
        public async Task<bool> CreateNewActivityAsync(Activities act)
        {
            await _mudeboDb.Activities.AddAsync(act);
            try
            {
                await _mudeboDb.SaveChangesAsync();
            }
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

        [HttpPut("{activityId}")] // PUT api/<ActivitiesController>/5
        [Authorize(Roles = "Admin")]
        public async Task<bool> EditActivityAsync(int activityId, Activities activityUpdated)
        {
            try
            {
                var act = await _mudeboDb.Activities.FindAsync(activityId);
                if(act != null)
                { await _activity.EditActivitiesAsync(act, activityUpdated);} 
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

        [HttpDelete("{id}")] // DELETE api/<ActivitiesController>/5
        [Authorize(Roles = "Admin")]
        public async Task<bool> DeleteActivity(int ActivityId)
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

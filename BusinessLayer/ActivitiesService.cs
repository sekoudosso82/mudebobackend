using Microsoft.EntityFrameworkCore;
using ModelsLayer;
using DbLayer;
using DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ActivitiesService : IActivities
    {
        private readonly MudeboDb _context;
        //constructor
        public ActivitiesService (MudeboDb context)
        {
            _context = context;
        }
        public async Task<bool> AddActivitiesAsync(Activities activity)
        {
            await _context.AddAsync(activity);

            try
            { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteActivitiesAsync(int activitiesId)
        {
            var act = await _context.Activities.FindAsync(activitiesId);
            try
            {
                if (act != null)
                {
                    _context.Activities.Remove(act);
                    await _context.SaveChangesAsync();
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

        public async Task<bool> EditActivitiesAsync(Activities activities, Activities activitiesUpdated)
        {
            var act = await _context.Activities.FindAsync(activities.ActivityId);
            try
            {
                if (act != null)
                {
                    act.ActivityId = activitiesUpdated.ActivityId;
                    act.ActivityTitle = activitiesUpdated.ActivityTitle;
                    act.ActivityDescription = activitiesUpdated.ActivityDescription;
                    act.ActivityDate = activitiesUpdated.ActivityDate;
                    act.ActivityStatus = activitiesUpdated.ActivityStatus;
                    act.ActivityPhoto = activitiesUpdated.ActivityPhoto;
                }
                await _context.SaveChangesAsync();
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

        public async Task<Activities?> FindActivitiesAsync(int actId)
        {
            var result = new Activities();
            var noResult = new Activities();
            try
            {
                var act = await _context.Activities.SingleOrDefaultAsync(x => x.ActivityId == actId);
                if (act != null) { result = act; }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"there was a problem finding this member => {ex.InnerException}");
                return noResult;
            }
            return result;
        }

        public async Task<List<Activities>> ActivitiesListAsync()
        {
            List<Activities> noResult = new List<Activities>();
            var actList = new List<Activities>();
            try
            {
                if (await _context.Activities.ToListAsync() is not null)
                { actList = await _context.Activities.ToListAsync(); }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"There was a problem getting activities list => {ex.InnerException}");
                return noResult;
            }
            return actList;
        }
    }
}

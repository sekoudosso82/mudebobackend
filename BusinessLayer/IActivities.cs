using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IActivities
    {
        Task<List<ActivityService>> ActivitiesListAsync();
        Task<ActivityService?> FindActivityAsync(int Activity);
        Task<bool> AddActivityAsync(ActivityService activity);
        Task<bool> EditActivityAsync(ActivityService activity, ActivityService activityUpdated);
        Task<bool> DeleteActivityAsync(int activityId);
    }
}

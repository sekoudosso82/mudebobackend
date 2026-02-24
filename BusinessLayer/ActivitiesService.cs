using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ActivityService : IActivities
    {
        Task<List<ActivityService>> IActivities.ActivitiesListAsync()
        {
            throw new NotImplementedException();
        }

        Task<bool> IActivities.AddActivityAsync(ActivityService activity)
        {
            throw new NotImplementedException();
        }

        Task<bool> IActivities.DeleteActivityAsync(int activityId)
        {
            throw new NotImplementedException();
        }

        Task<bool> IActivities.EditActivityAsync(ActivityService activity, ActivityService activityUpdated)
        {
            throw new NotImplementedException();
        }

        Task<ActivityService?> IActivities.FindActivityAsync(int Activity)
        {
            throw new NotImplementedException();
        }
    }
}

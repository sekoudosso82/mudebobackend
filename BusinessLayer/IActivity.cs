using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IActivity
    {
        Task<List<Activity>> ActivitiesListAsync();
        Task<Activity?> FindActivityAsync(int Activity);
        Task<bool> AddActivityAsync(Activity activity);
        Task<bool> EditActivityAsync(Activity activity, Activity activityUpdated);
        Task<bool> DeleteActivityAsync(int activityId);
    }
}

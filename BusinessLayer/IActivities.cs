using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IActivities
    {
        Task<List<Activities>> ActivitiesListAsync();
        Task<Activities?> FindActivitiesAsync(int Activity);
        Task<bool> AddActivitiesAsync(Activities activity);
        Task<bool> EditActivitiesAsync(Activities activities, Activities activitiesUpdated);
        Task<bool> DeleteActivitiesAsync(int activitiesId);
    }
}

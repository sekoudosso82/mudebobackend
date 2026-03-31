using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IProjects
    {
        Task<List<Projects>> ProjectsListAsync();
        Task<Projects?> FindProjectAsync(int projectId);
        Task<bool> AddProjectAsync(Projects project);
        Task<bool> EditProjectAsync(Projects project, Projects projectUpdated);
        Task<bool> DeleteProjectAsync(int projectId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IProject
    {
        Task<List<ProjectService>> ProjectListAsync();
        Task<ProjectService?> FindProjectAsync(int projectId);
        Task<bool> AddProjectAsync(ProjectService project);
        Task<bool> EditProjectAsync(ProjectService project, ProjectService projectUpdated);
        Task<bool> DeleteProjectAsync(int projectId);
    }
}

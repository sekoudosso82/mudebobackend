using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IProject
    {
        Task<List<Project>> ProjectListAsync();
        Task<Project?> FindProjectAsync(int projectId);
        Task<bool> AddProjectAsync(Project project);
        Task<bool> EditProjectAsync(Project project, Project projectUpdated);
        Task<bool> DeleteProjectAsync(int projectId);
    }
}

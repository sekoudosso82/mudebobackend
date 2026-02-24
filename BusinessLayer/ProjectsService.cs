using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ProjectService : IProject
    {
        Task<bool> IProject.AddProjectAsync(ProjectService project)
        {
            throw new NotImplementedException();
        }

        Task<bool> IProject.DeleteProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        Task<bool> IProject.EditProjectAsync(ProjectService project, ProjectService projectUpdated)
        {
            throw new NotImplementedException();
        }

        Task<ProjectService?> IProject.FindProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        Task<List<ProjectService>> IProject.ProjectListAsync()
        {
            throw new NotImplementedException();
        }
    }
}

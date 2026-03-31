using DbLayer;
using Microsoft.EntityFrameworkCore;
using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ProjectsService : IProjects
    {

        private readonly MudeboDb _context;
        //constructor
        public ProjectsService(MudeboDb context)
        {
            _context = context;
        }
        public async Task<bool> AddProjectAsync(Projects project)
        {

            await _context.AddAsync(project);

            try
            { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            var p = await _context.Projects.FindAsync(projectId);
            try
            {
                if (p != null)
                {
                    _context.Projects.Remove(p);
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

        public async Task<bool> EditProjectAsync(Projects project, Projects projectUpdated)
        {
            var p = await _context.Projects.FindAsync(project.ProjectId);
            try
            {
                if (p != null)
                {
                    p.ProjectTitle = projectUpdated.ProjectTitle;
                    p.ProjectDescription = projectUpdated.ProjectDescription;
                    p.ProjectStatus = projectUpdated.ProjectStatus;
                    p.ProjectDate = projectUpdated.ProjectDate;
                    p.ProjectPhoto = projectUpdated.ProjectPhoto;
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

        public async Task<Projects?> FindProjectAsync(int projectId)
        {
            var result = new Projects();
            var noResult = new Projects();
            try
            {
                var p = await _context.Projects.SingleOrDefaultAsync(x => x.ProjectId == projectId);
                if (p != null) { result = p; }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"there was a problem finding this project => {ex.InnerException}");
                return noResult;
            }
            return result;
        }

        public async Task<List<Projects>> ProjectsListAsync()
        {
            List<Projects> noResult = new List<Projects>();
            var pList = new List<Projects>();
            try
            {
                if (await _context.Projects.ToListAsync() is not null)
                {
                    pList = await _context.Projects.ToListAsync();
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"there was a problem finding this member => {ex.InnerException}");
                return noResult;
            }
            return pList;
        }
    }
}

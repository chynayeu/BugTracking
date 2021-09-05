using BugTracking.DAL.Entities;
using BugTracking.Models;

namespace BugTracking.Services.Impl.Converters
{
    public class DBProjectModelConverter : IConverter<Project, ProjectModel>
    {
        public Project Convert(ProjectModel source)
        {
            Project result = new Project();
            result.Id = source.Id;
            result.name = source.Name;
            result.description = source.Description;
            return result;
        }

        public ProjectModel Convert(Project source)
        {
            ProjectModel result = new ProjectModel();
            result.Id = source.Id;
            result.Name = source.name;
            result.Description = source.description;
            return result;
        }
    }
}

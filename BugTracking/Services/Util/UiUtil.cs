using BugTracking.Models;
using System.Collections.Generic;

namespace BugTracking.Services.Util
{
    public static class UiUtil
    {
        public static bool isUserAdmin(UserModel user)
        {
            foreach(UserRoleModel role in user.Roles)
            {
                if (role.Name == "admin") return true;
            }
            return false;
        }

        public static ProjectModel GetProjectById(List<ProjectModel> projects, int id)
        {
            foreach(ProjectModel project in projects)
            {
                if (project.Id == id) return project;
            }
            return null;
        }

    }
}

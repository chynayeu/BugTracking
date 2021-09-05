using BugTracking.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;

namespace BugTracking.Services.Util
{
    public static class SessionUtil
    {
        private static string CUREENT_USER = "current_user";
        private static string CUREENT_PROJECTS = "current_projects";

        public static void SetCurrentUser(UserModel value, ISession session)
        {
            session.SetString(CUREENT_USER, JsonSerializer.Serialize(value));
        }
        
        public static UserModel GetCurrentUser(ISession session)
        {
            var value = session.GetString(CUREENT_USER);
            return value == null ? default : JsonSerializer.Deserialize<UserModel>(value);
        }

        public static void SetCurrentProjects(List<ProjectModel> value, ISession session)
        {
            session.SetString(CUREENT_PROJECTS, JsonSerializer.Serialize(value));
        }

        public static List<ProjectModel> GetCurrentProjects(ISession session)
        {
            var value = session.GetString(CUREENT_PROJECTS);
            return value == null ? default : JsonSerializer.Deserialize<List<ProjectModel>>(value);
        }
    }
}

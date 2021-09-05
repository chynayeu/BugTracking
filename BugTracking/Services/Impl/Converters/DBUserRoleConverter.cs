using BugTracking.DAL.Entities;
using BugTracking.Models;

namespace BugTracking.Services.Impl.Converters
{
    public class DBUserRoleConverter : IConverter<UserRole, UserRoleModel>
    {
        public UserRole Convert(UserRoleModel source)
        {
            UserRole role = new UserRole();
            role.Name = source.Name;
            return role;
        }

        public UserRoleModel Convert(UserRole source)
        {
            UserRoleModel role = new UserRoleModel(source.Id, source.Name);
            return role;
        }
    }
}

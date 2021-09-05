using BugTracking.DAL.Entities;
using BugTracking.Models;
using System.Collections.Generic;

namespace BugTracking.Services.Impl.Converters
{
    public class DBUserModelConverter : IConverter<User, UserModel>
    {
        public User Convert(UserModel source)
        {
            User user = new User();

            user.Id = source.Id;
            user.Email = source.Email;
            user.Name = source.Name;
            user.Surname = source.Surname;

            return user;
        }

        public UserModel Convert(User source)
        {
            UserModel user = new UserModel();

            user.Id = source.Id;
            user.Email = source.Email;
            user.Name = source.Name;
            user.Surname = source.Surname;

            if(source.Roles != null)
            {
                user.Roles = new List<UserRoleModel>();
                foreach(UserRole role in source.Roles)
                {
                    user.Roles.Add(new UserRoleModel(role.Id, role.Name));
                }
            }

            return user;
        }
    }
}

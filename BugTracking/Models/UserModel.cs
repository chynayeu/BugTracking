using System;
using System.Collections.Generic;

namespace BugTracking.Models
{
    /// <summary>
    /// Модель пользователя для контроллера
    /// </summary>
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Checked { get; set; }

        public List<UserRoleModel> Roles { get; set; }

        public void updateRoles(List<string> newRoles)
        {
            Roles.Clear();

            newRoles.ForEach(r => Roles.Add(new UserRoleModel(r)));

        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            UserModel model = obj as UserModel;
            if (model == null) return false;
            else return this.Email == model.Email;
        }
        public override int GetHashCode()
        {
            return Email.GetHashCode();
        }
    }
}

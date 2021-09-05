using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracking.Models
{
    public class UserRoleModel
    {
        public UserRoleModel()
        {

        }

        public UserRoleModel(int id, string name) : this(name)
        {
            Id = id;
        }

        public UserRoleModel(string name)
        {
             Name = name;
        }
         public int Id { get; set; }

         public string Name { get; set; }
    }
}

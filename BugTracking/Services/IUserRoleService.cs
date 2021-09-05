using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracking.Services
{
    public interface IUserRoleService<R>
    {
        /// <summary>
        /// Метод для создания роли пользователя
        /// </summary>
        /// <param name="role">роль для создания</param>
        public bool CreateRole(R role);

        /// <summary>
        /// Мутод для получения всех ролей
        /// </summary>
        /// <returns></returns>
        public List<R> ReadRoles();

        /// <summary>
        /// Метод для удаления роли
        /// </summary>
        /// <param name="role">роль для удаления</param>
        public bool DeleteRole(R role);

        /// <summary>
        /// Обновление роли 
        /// </summary>
        /// <param name="role">обновленная роль</param>
        public bool UpdateRole(R role);
    }
}

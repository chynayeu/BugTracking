using BugTracking.DAL.Data;
using BugTracking.DAL.Entities;
using BugTracking.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracking.Services.Impl
{
    public class DBUserRoleServiceImpl : IUserRoleService<UserRoleModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IConverter<UserRole, UserRoleModel> _converter;
        public DBUserRoleServiceImpl(ApplicationDbContext context, ILogger<DBUserRoleServiceImpl> logger,
            IConverter<UserRole, UserRoleModel> converter)
        {
            _context = context;
            _logger = logger;
            _converter = converter;
        }
        public bool CreateRole(UserRoleModel role)
        {
            try
            {
                _context.UserRoles.Add(_converter.Convert(role));
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка добавления роли : " + e.Message);
                return false;
            }
            return true;
        }

        public bool DeleteRole(UserRoleModel role)
        {
            try
            {
                UserRole toDelete = _context.UserRoles.SingleOrDefault(r => r.Id == role.Id);
                if (toDelete != default)
                {
                    _context.UserRoles.Remove(toDelete);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка удаления роли : " + e.Message);
                return false;
            }
            return true;
        }

        public List<UserRoleModel> ReadRoles()
        {
            List<UserRoleModel> result = new List<UserRoleModel>();

            try
            {
                _context.UserRoles.ToList().ForEach(r => result.Add(_converter.Convert(r)));
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка получения ролей : " + e.Message);
                return null;
            }

            return result;
        }

        public bool UpdateRole(UserRoleModel role)
        {
            try
            {
                UserRole toUpdate = _context.UserRoles.SingleOrDefault(r => r.Id == role.Id);
                if (toUpdate != default)
                {
                    toUpdate.Name = role.Name;
                    _context.SaveChanges();
                }
                
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка изменения роли : " + e.Message);
                return false;
            }
            return true;
        }
    }
}

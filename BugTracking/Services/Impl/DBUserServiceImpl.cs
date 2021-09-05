using BugTracking.DAL.Data;
using BugTracking.DAL.Entities;
using BugTracking.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracking.Services.Impl
{
    public class DBUserServiceImpl : IUserService<UserModel, UserRoleModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IConverter<User, UserModel> _converter;

        public DBUserServiceImpl(ApplicationDbContext context, ILogger<DBUserServiceImpl> logger,
            IConverter<User, UserModel> converter)
        {
            _context = context;
            _logger = logger;
            _converter = converter;
        }

        public bool CreateUser(string name, string surname, string email, List<UserRoleModel> userRoles)
        {
            User user = new User();
            user.Name = name;
            user.Surname = surname;
            user.Email = email;
            user.Roles = new List<UserRole>();

            try
            {
                userRoles.ToList().ForEach(r =>
                {
                    UserRole role = _context.UserRoles.SingleOrDefault(e => e.Name == r.Name);
                    if (role == default)
                    {
                        role = new UserRole();
                        role.Name = r.Name;
                        _context.UserRoles.Add(role);
                    }
                    user.Roles.Add(role);
                });
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка добавления пользователя : " + e.Message);
                return false;
            }
            return true;
        }

        public UserModel ReadUser(string userEmail)
        {

            UserModel user;
            try
            {
                User dbUser = _context.Users.SingleOrDefault(u => u.Email == userEmail);
                _context.Entry(dbUser).Collection(u => u.Roles).Load();
                user = _converter.Convert(dbUser);

            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка удаления пользователя : " + e.Message);
                return null;
            }

            return user;
        }

        public bool UpdateUser(UserModel user)
        {

            try
            {
                User userToUpdate = _context.Users.SingleOrDefault(u => u.Id == user.Id);
                _context.Entry(userToUpdate).Collection(u => u.Comments).Load();
                _context.Entry(userToUpdate).Collection(u => u.Projects).Load();
                _context.Entry(userToUpdate).Collection(u => u.Roles).Load();
                userToUpdate.Roles.Clear();
                copyUser(userToUpdate, _converter.Convert(user));
                if (userToUpdate != null)
                {
                    user.Roles.ToList().ForEach(r =>
                    {
                        UserRole role = _context.UserRoles.SingleOrDefault(e => e.Name == r.Name);
                        //if no such role - create new and add to the table
                        if(role == default)
                        {
                            role = new UserRole();
                            role.Name = r.Name;
                            _context.UserRoles.Add(role);
                        }
                        userToUpdate.Roles.Add(role);
                    });
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка обнавления пользователя : " + e.Message);
                return false;
            }
            return true;
        }

        public bool DeleteUser(UserModel user)
        {
            try
            {
                User toDelete = _context.Users.SingleOrDefault(u => u.Email == user.Email);
                _context.Users.Remove(toDelete);
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка удаления пользователя : " + e.Message);
                return false;
            }
            return true;
        }

        public List<UserModel> AllUsers()
        {
            List<UserModel> result = new List<UserModel>();

            try
            {
                _context.Users.ToList().ForEach(u => result.Add(_converter.Convert(u)));
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка получения пользователей : " + e.Message);
                return null;
            }

            return result;
        }

        /// <summary>
        /// Метод для копирования свойств пользователя
        /// </summary>
        /// <param name="userTo">Пользователь, куда копировать</param>
        /// <param name="userFrom">Пользователь из которого копировать</param>
        private static void copyUser(User userTo, User userFrom)
        {
            userTo.Name = userFrom.Name;
            userTo.Surname = userFrom.Surname;
            userTo.Email = userFrom.Email;
        }
    }
}

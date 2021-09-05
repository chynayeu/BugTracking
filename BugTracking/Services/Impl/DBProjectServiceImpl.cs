using BugTracking.DAL.Data;
using BugTracking.DAL.Entities;
using BugTracking.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracking.Services.Impl
{
    public class DBProjectServiceImpl : IProjectService<ProjectModel, UserModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IConverter<Project, ProjectModel> _converter;
        private readonly IConverter<User, UserModel> _userConverter;

        public DBProjectServiceImpl(ApplicationDbContext context, ILogger<DBProjectServiceImpl> logger, 
            IConverter<Project, ProjectModel> converter, IConverter<User, UserModel> userConverter)
        {
            _context = context;
            _logger = logger;
            _converter = converter;
            _userConverter = userConverter;
        }
        public bool CreateProject(string name, string description)
        {
            Project project = new Project();
            project.name = name;
            project.description = description;
            try
            {
                _context.Projects.Add(project);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка добавления проекта : " + e.Message);
                return false;
            }
            return true;
        }

        public bool DeleteProject(ProjectModel project)
        {
            try
            {
                if (project != null)
                {
                    Project prj = _context.Projects.SingleOrDefault(p => p.Id == project.Id);
                    _context.Projects.Remove(prj);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка удаления проекта : " + e.Message);
                return false;
            }
            return true;
        }

        public List<ProjectModel> ReadUserProjects(string userEmail)
        {
            List<ProjectModel> result = new List<ProjectModel>();
            try
            {
                User userFromDb = _context.Users.SingleOrDefault(u => u.Email == userEmail);
                _context.Entry(userFromDb).Collection(u => u.Projects).Load();


                if (userFromDb.Projects != null)
                {
                    userFromDb.Projects.ToList().ForEach(p => result.Add(_converter.Convert(p)));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка получения проектов : " + e.Message);
                return null;
            }
            return result;
        }

        public List<ProjectModel> ReadAllProjects()
        {
            List<ProjectModel> result = new List<ProjectModel>();
            try
            {
                _context.Projects.ToList().ForEach(p => result.Add(_converter.Convert(p)));
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка получения проектов : " + e.Message);
                return null;
            }
            return result;
        }

        public bool UpdateProject(ProjectModel project)
        {
            try
            {
                if (project != null)
                {
                    Project projectToUpdate = _context.Projects.SingleOrDefault(p => p.Id == project.Id);
                    _context.Entry(projectToUpdate).Collection(p => p.Users).Load();
                    /*             projectToUpdate.Users.Clear();
                                 foreach (UserModel user in project.Users)
                                 {
                                     projectToUpdate.Users.Add(_context.Users.SingleOrDefault(u => u.Email == user.Email));
                                 }*/
                    _context.Entry(projectToUpdate).Collection(p => p.Tickets).Load();
                    copyProjects(projectToUpdate, _converter.Convert(project));
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка обновления проекта : " + e.Message);
                return false;
            }
            return true;
        }

        public List<UserModel> GetUsersForProject(int projectId)
        {
            List<UserModel> result = new List<UserModel>();
            try
            {
                Project project = _context.Projects.SingleOrDefault(p => p.Id == projectId);
                _context.Entry(project).Collection(p => p.Users).Load();
                if (project.Users != null)
                {
                    project.Users.ToList().ForEach(u => result.Add(_userConverter.Convert(u)));
                }            
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка получения пользователей : " + e.Message);
                return null;
            }
            return result;
        }

        public bool UpdateUsersForProject(int projectId, List<string> userEmails)
        {
            try
            {
                Project projectToUpdate = _context.Projects.SingleOrDefault(p => p.Id == projectId);
                _context.Entry(projectToUpdate).Collection(p => p.Users).Load();
                if (projectToUpdate.Users != null) projectToUpdate.Users.Clear();

                userEmails.ToList().ForEach(e =>
                {
                    projectToUpdate.Users.Add(_context.Users.SingleOrDefault(u => u.Email == e));
                });
                _context.Entry(projectToUpdate).Collection(p => p.Tickets).Load();
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка обновления проекта : " + e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Вспомогательный метод для копирование свойств проекта
        /// </summary>
        /// <param name="projectTo">проект в который кпировать</param>
        /// <param name="projectFrom">проект из которого копирование</param>
        private static void copyProjects(Project projectTo, Project projectFrom)
        {
            projectTo.name = projectFrom.name;
            projectTo.description = projectFrom.description;
        }

    }
}

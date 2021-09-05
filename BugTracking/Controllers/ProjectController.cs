using BugTracking.DAL.Entities;
using BugTracking.Models;
using BugTracking.Services;
using BugTracking.Services.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracking.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService<ProjectModel, UserModel> _projectService;
        private readonly ILogger<ProjectController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService<UserModel, UserRoleModel> _userService;

        public ProjectController(ILogger<ProjectController> logger, IProjectService<ProjectModel, UserModel> projectService,
            UserManager<ApplicationUser> userManager, IUserService<UserModel,UserRoleModel> userService)
        {
            _userManager = userManager;
            _logger = logger;
            _projectService = projectService;
            _userService = userService;
        }

        [Authorize(Roles = "admin,developer,tester,manager")]
        public async Task<ActionResult> IndexAsync()
        {
            List<ProjectModel> projects;
            UserModel currentUser = SessionUtil.GetCurrentUser(HttpContext.Session);

            if(currentUser == null)
            {
                var user = await _userManager.GetUserAsync(User);
                currentUser = _userService.ReadUser(user.Email);
                SessionUtil.SetCurrentUser(currentUser, HttpContext.Session);
            }

            if (UiUtil.isUserAdmin(currentUser))
            {
                projects = _projectService.ReadAllProjects();
            }
            else projects = _projectService.ReadUserProjects(currentUser.Email);

            SessionUtil.SetCurrentProjects(projects, HttpContext.Session);
            return View(projects);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string Name, string Description)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                _projectService.CreateProject(Name, Description);
            }
            return RedirectToAction(nameof(Index)); ;
        }


        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            List<ProjectModel> projects = GetProjects();
            ViewData["Users"] = _projectService.GetUsersForProject(id);
            return View(UiUtil.GetProjectById(projects, id));
        }

        // POST: ProjectController/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectModel project)
        {
            if (_projectService.UpdateProject(project))
            {
                // TODO: optimize for use project from session instead of service
            }
            return RedirectToAction("Index", "Project");
        }

        [Authorize(Roles = "admin")]
        public ActionResult UserList(int id)
        {
            ProjectModel project = UiUtil.GetProjectById(GetProjects(), id);
            ViewData["Project"] = project;
            List<UserModel> allUsers = _userService.AllUsers();
            List<UserModel> projectUsers = _projectService.GetUsersForProject(id);
            allUsers.ForEach(u =>
            {
                if (projectUsers.Contains(u)) u.Checked = true;
            });
            return View(allUsers);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUsers(int projectId, List<string> users)
        {
            _projectService.UpdateUsersForProject(projectId, users);
            return RedirectToAction("Index", "Project");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            List<ProjectModel> projects = SessionUtil.GetCurrentProjects(HttpContext.Session);
            if(_projectService.DeleteProject(UiUtil.GetProjectById(projects, id)))
            {
                projects.Remove(UiUtil.GetProjectById(projects, id));
                SessionUtil.SetCurrentProjects(projects, HttpContext.Session);
            }
            return RedirectToAction("Index", "Project");
        }


        public List<ProjectModel> GetProjects()
        {
            return SessionUtil.GetCurrentProjects(HttpContext.Session);
        }
    }
}

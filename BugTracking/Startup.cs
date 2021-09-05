using BugTracking.DAL.Data;
using BugTracking.DAL.Entities;
using BugTracking.Models;
using BugTracking.Services;
using BugTracking.Services.Impl;
using BugTracking.Services.Impl.Converters;
using EnglishCards.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BugTracking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
            });


            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();

            services.AddControllersWithViews();

            //Services
            services.AddScoped<IProjectService<ProjectModel, UserModel>, DBProjectServiceImpl>();
            services.AddScoped<ITicketService<TicketModel, ProjectModel, CommentModel>, DBTicketServiceImpl>();
            services.AddScoped<IUserService<UserModel, UserRoleModel>, DBUserServiceImpl>();
            services.AddScoped<IUserRoleService<UserRoleModel>, DBUserRoleServiceImpl>();

            //Converters
            services.AddScoped<IConverter<Project, ProjectModel>, DBProjectModelConverter>();
            services.AddScoped<IConverter<Ticket, TicketModel>, DBTicketModelConverter>();
            services.AddScoped<IConverter<User, UserModel>, DBUserModelConverter>();
            services.AddScoped<IConverter<Comment, CommentModel>, DBCommentModelConverter>();
            services.AddScoped<IConverter<UserRole, UserRoleModel>, DBUserRoleConverter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserService<UserModel, UserRoleModel> userService)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            DbInitializer.Seed(context, userManager, roleManager, userService).GetAwaiter().GetResult();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using FrontEnd2015MVC.Models.Roles;
using SharedDto;
using WebMatrix.WebData;

namespace FrontEnd2015MVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            WebSecurity.InitializeDatabaseConnection("UniverseConnection", "Users", "Id", "UserName", true);
            var roles = (SimpleRoleProvider) Roles.Provider;

            if (!roles.RoleExists(UsersRoles.CanBanUsers))
                roles.CreateRole(UsersRoles.CanBanUsers);
            if (!roles.RoleExists(UsersRoles.CanCreateStars))
                roles.CreateRole(UsersRoles.CanCreateStars);
            if (!roles.RoleExists(UsersRoles.CanEditUsers))
                roles.CreateRole(UsersRoles.CanEditUsers);
            if (!roles.RoleExists(UsersRoles.CanModifyStats))
                roles.CreateRole(UsersRoles.CanModifyStats);
            if (!roles.RoleExists(UsersRoles.CanPurgeLog))
                roles.CreateRole(UsersRoles.CanPurgeLog);
            if (!roles.RoleExists(UsersRoles.CanViewLog))
                roles.CreateRole(UsersRoles.CanViewLog);
            if (!roles.RoleExists(UsersRoles.CanViewUsers))
                roles.CreateRole(UsersRoles.CanViewUsers);
            if (!roles.RoleExists(UsersRoles.CanAccessAdministration))
                roles.CreateRole(UsersRoles.CanAccessAdministration);
            if (!roles.RoleExists(UsersRoles.CanCreateRace))
                roles.CreateRole(UsersRoles.CanCreateRace);
            if (!roles.RoleExists(UsersRoles.CanModifyStatus))
                roles.CreateRole(UsersRoles.CanModifyStatus);

            if (WebSecurity.UserExists(ConfigurationManager.AppSettings[ConfAppSettings.AdminUsername])) return;
            WebSecurity.CreateUserAndAccount(
                ConfigurationManager.AppSettings[ConfAppSettings.AdminUsername],
                ConfigurationManager.AppSettings[ConfAppSettings.AdminPassword],
                new
                {
                    Email = ConfigurationManager.AppSettings[ConfAppSettings.AdminEmail],
                    ScoreConstruction = 0,
                    ScoreResearch = 0,
                    ScoreMilitary = 0,
                    ScoreCultural = 0,
                    Status = 1,
                    RaceName = "Amministrazione",
                    RacePointsUsed = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
                );
            var roleList = new List<string>
            {
                UsersRoles.CanAccessAdministration,
                UsersRoles.CanBanUsers,
                UsersRoles.CanCreateStars,
                UsersRoles.CanEditUsers,
                UsersRoles.CanModifyStats,
                UsersRoles.CanPurgeLog,
                UsersRoles.CanViewLog,
                UsersRoles.CanViewUsers,
                UsersRoles.CanModifyStatus
            };
            var administrators = new List<string>
            {
                ConfigurationManager.AppSettings[ConfAppSettings.AdminUsername]
            };
            roles.AddUsersToRoles(administrators.ToArray(), roleList.ToArray());
        }
    }
}
using FrontEnd2015MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace FrontEnd2015MVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            try
            {
                WebSecurity.InitializeDatabaseConnection("UniverseConnection", "Users", "Id", "UserName", autoCreateTables: true);
                var roles = (SimpleRoleProvider)System.Web.Security.Roles.Provider;
                var membership = (SimpleMembershipProvider)System.Web.Security.Membership.Provider;

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
            }
            catch (Exception ex)
            {
                //DAFARE : Creare un progetto per gestire tutti i log in modo efficente
                throw;
            }
        }
    }
}
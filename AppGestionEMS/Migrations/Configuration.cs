namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AppGestionEMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppGestionEMS.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            string roleAdmin = "admin";
            string roleProfe = "profesor";
            string roleAlum = "alumno";
            AddRole(context, roleAdmin);
            AddRole(context, roleProfe);
            AddRole(context, roleAlum);
            AddUser(context, "admin", "admin", "admin@upm.es", roleAdmin);
            AddUser(context, "profesor1", "apell", "profesor1@upm.es", roleProfe);
            AddUser(context, "profesor2", "apell", "profesor2@upm.es", roleProfe);
            AddUser(context, "ficitio1", "apell", "ficticio1@alumnos.upm.es", roleAlum);
            AddUser(context, "ficitio2", "apell", "ficticio2@alumnos.upm.es", roleAlum);
            AddUser(context, "ficitio3", "apell", "ficticio3@alumnos.upm.es", roleAlum);
        }
        public void AddRole(ApplicationDbContext context, String role)
        {
            IdentityResult IdRoleResult;
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            if (!roleMgr.RoleExists(role))
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = role });
        }
        public void AddUser(ApplicationDbContext context, String name, String surname, String email, String role)
        {
            IdentityResult IdUserResult;
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                Nombre = name,
                Apellidos = surname,
                UserName = email,
                Email = email,
            };
            IdUserResult = userMgr.Create(appUser, "123456Aa!");
            //asociar usuario con role
            if (!userMgr.IsInRole(userMgr.FindByEmail(email).Id, role))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail(email).Id, role);
            }
        }
    }
}

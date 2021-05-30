using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AppGestionEMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual string Nombre { get; set; }
        public virtual string Apellidos { get; set; }
        public virtual string DNI { get; set; }
        public virtual string NMat { get; set; }
        public virtual string CodP { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Cursos> Cursos { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Grupos> Grupos { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.AsignaciónDocentes> AsignaciónDocentes { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Matriculas> Matriculas { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Evaluaciones> Evaluaciones { get; set; }

    }
}
using Microsoft.EntityFrameworkCore;
using PermissionsAPI;

namespace PermissionsAPI.Models
{
    public class PermisosContext:DbContext
    {
        public PermisosContext(DbContextOptions<PermisosContext> options):base(options)
        {

        }

        public DbSet<Permisos> Permisos { get; set; } = null!;
    }
}

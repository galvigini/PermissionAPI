using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PermissionsAPI;
using PermissionsAPI.Models;

namespace PermissionsAPI.Models
{
    public class  TipoPermisosContext : DbContext
    {
        public TipoPermisosContext(DbContextOptions<TipoPermisosContext> options) : base(options)
        {

        }
        public DbSet<TipoPermisos> TipoPermisos { get; set; } = null!;
    }
}

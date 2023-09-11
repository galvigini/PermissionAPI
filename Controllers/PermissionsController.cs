using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using PermissionsAPI.Models;

namespace PermissionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly  PermisosContext _permisosContext;
        public PermissionsController(PermisosContext permisosContext)
        {
            _permisosContext = permisosContext;
        }

        [HttpPost]
        public async Task<ActionResult<Permisos>> RequestPermission(Permisos permisos)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
               .DefaultIndex("permissions");


            if (_permisosContext == null)
            {
                return NotFound();
            }

            _permisosContext.Permisos.Add(permisos);

            await _permisosContext.SaveChangesAsync();

            var client = new ElasticClient(settings);

            var indexResponse = client.IndexDocument(permisos);


            return permisos;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permisos>>> GetPermission()
        {
            if (_permisosContext == null)
            {
                return NotFound();
            }
            return await _permisosContext.Permisos.ToListAsync();
        }

        //GET : api/permisos/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Permisos>> GetPermission(int id)
        {
            if (_permisosContext == null)
            {
                return NotFound();
            }

            var permisos = await _permisosContext.Permisos.FindAsync(id);
            if (permisos == null)
            {
                return NotFound();
            }
            return permisos;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Permisos>> ModifyPermission(int id, Permisos permisos)
        {
            if (permisos.Id != id) 
            {
                return BadRequest();
            }

            _permisosContext.Entry(permisos).State = EntityState.Modified;

            await _permisosContext.SaveChangesAsync();

            var updatedPermisos = _permisosContext.Permisos.FirstOrDefaultAsync(x => x.Id == id);

            return permisos;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var permisos = await _permisosContext.Permisos.FindAsync(id);

            if (permisos == null) return NotFound();

            _permisosContext.Permisos.Remove(permisos);

            await _permisosContext.SaveChangesAsync();

            return NoContent();

        }
    }
}

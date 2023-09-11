using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PermissionsAPI.Controllers;
using PermissionsAPI.Models;
using Xunit;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace PermissionsAPI.Tests
{
    public class PermissionsControllerTests
    {
        private readonly PermissionsController _controller;

        public PermissionsControllerTests()
        {
            // Configura una base de datos en memoria para las pruebas
            var options = new DbContextOptionsBuilder<PermisosContext>()
                .UseSqlServer( )
                .Options;

            var context = new PermisosContext(options);

            _controller = new PermissionsController(context);
        }

        [Fact]
        public async Task RequestPermission_ReturnsCreatedAtActionResult()
        {

            var permisos = new Permisos {
                 Id=46,
                 NombreEmpleado="Gerardo",
                 ApellidoEmpleado="Alvigini",
                 TipoPermiso=3,
            };

   
            var result = await _controller.RequestPermission(permisos);

      
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Permisos>(createdAtActionResult.Value);


        }

    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarjetaBA.Server.Models;

namespace TarjetaBA.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly TarjetaContext _context;

        public ComprasController(TarjetaContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> PostCompra(Compra compra)
        {
            //Se llama al procedimiento almacenado en PLSQL de CrearCompra tiene que aplicar el script
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC CrearCompra @IdTarjeta={compra.IdTarjeta},@Cuenta={compra.Cuenta},@Nombre={compra.Nombre},@Apellido={compra.Apellido},@Fecha = {compra.Fecha}, @Descripcion = {compra.Descripcion}, @Monto = {compra.Monto}");

            return Ok();
        }
        [HttpGet]
        [Route("ListaCompra")]
        public async Task<ActionResult<IEnumerable<Compra>>> ListaCompra()
        {
            //Se llama al procedimiento almacenado en PLSQL de ObtenerListaCompras tiene que aplicar el script
            var compras = await _context.Compras.FromSqlRaw("EXEC ObtenerListaCompras").ToListAsync();

            return Ok(compras);
        }
    }
}

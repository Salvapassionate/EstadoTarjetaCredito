
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TarjetaBA.Server.Models;

namespace TarjetaBA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly TarjetaContext _context;

        public PagosController(TarjetaContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> PostPago(Pago pago)
        {
            //Se llama al procedimiento almacenado en PLSQL de CrearPago
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC CrearPago @IdCompra={pago.IdCompra},@IdTarjeta={pago.IdTarjeta},@Cuenta={pago.Cuenta},@Nombre={pago.Nombre},@Apellido={pago.Apellido},@Fecha = {pago.Fecha}, @Descripcion = {pago.Descripcion}, @Monto = {pago.Monto},@Abono = {pago.Abono}");

            return Ok();
        }
        [HttpGet]
        [Route("ListaPago")]
        public async Task<ActionResult<IEnumerable<Pago>>> ListaPago()
        {
            //Se llama al procedimiento almacenado en PLSQL de ObtenerListaCompras
            var pagos = await _context.Pagos.FromSqlRaw("EXEC ObtenerListaPagos").ToListAsync();

            return Ok(pagos);
        }
    }
}
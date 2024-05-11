using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarjetaBA.Server.Models;


namespace TarjetaCredito.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasCreditosController : ControllerBase
    {
        private readonly TarjetaContext _context;
        public TarjetasCreditosController(TarjetaContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearTarjeta(Tarjeta tarjeta)
        {
            //Se llama al procedimiento almacenado en PLSQL de CrearTarjeta
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC CrearTarjeta @Cuenta = {tarjeta.Cuenta}, @Nombre = {tarjeta.Nombre}, @Apellido = {tarjeta.Apellido}");

            return Ok();
        }
        [HttpGet]
        [Route("Lista")]
        public async Task<ActionResult<IEnumerable<Tarjeta>>> ListaTarjeta()
        {
            //Se llama al procedimiento almacenado en PLSQL de ObtenerListaTarjetas
            var tarjetas = await _context.Tarjetas.FromSqlRaw("EXEC ObtenerListaTarjetas").ToListAsync();

            return Ok(tarjetas);
        }
    }
}

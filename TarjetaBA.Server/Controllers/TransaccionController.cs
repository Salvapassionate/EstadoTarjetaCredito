using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarjetaBA.Server.Models;

namespace TarjetaBA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly TarjetaContext _context;

        public TransaccionController(TarjetaContext context)
        {
            _context = context;
        }

        [HttpGet]
        //Aclaracion estaba en plan de obtener datos de solicitados para presentar como Transaccion pero parcialmente no muestra
        //le falta el desarrollo y estaba planeado
        public async Task<ActionResult<Transaccion>> GetTransaccion()
        {
            // Obtener todas las compras y pagos
            var compras = await _context.Compras.FirstOrDefaultAsync();
            var pagos = await _context.Pagos.FirstOrDefaultAsync();


            // Obtener la cuenta y el nombre asociados al estado de cuenta
            var tarjeta = await _context.Tarjetas.FirstOrDefaultAsync();

            // Crear el objeto EstadoCuenta con los datos calculados y la cuenta y nombre asociados
            var transaccion = new Transaccion
            {
                Titular = tarjeta.Nombre + " " + tarjeta.Apellido,
                NumeroTarjeta = tarjeta.Cuenta,
                Fecha = pagos.Fecha,
                Descripcion= compras.Descripcion,
                Cargo= compras.Monto,
                Abono = pagos.Abono,
               
            };

            // Agregar la cuenta y el nombre al objeto devuelto
            var transaccionResponse = new
            {
                transaccion.Titular,
                transaccion.NumeroTarjeta,
                transaccion.Fecha,
                transaccion.Descripcion,
                transaccion.Cargo,
                transaccion.Abono,
            };

            return Ok(transaccionResponse);
        }
    }
}

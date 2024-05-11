using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarjetaBA.Server.Models;

namespace TarjetaBA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCuentaController : ControllerBase
    {
        private readonly TarjetaContext _context;

        public EstadoCuentaController(TarjetaContext context)
        {
            _context = context;
        }
        //Aclaracion estaba en plan de obtener datos con api get de solicitados para presentar como Estado Cuenta pero parcialmente no muestra
        //le falta el desarrollo y estaba planeado
        [HttpGet]
        public async Task<ActionResult<EstadoCuenta>> GetEstadoCuenta(int id)
        {

            // Obtener todas las compras y pagos
            var compras = await _context.Compras.ToListAsync();
            var pagos = await _context.Pagos.ToListAsync();

            // Calcular el saldo actual sumando las compras y restando los pagos
            decimal saldoActual = compras.Sum(c => c.Monto) - pagos.Sum(p => p.Abono);
            //Calcular el limite
            decimal limiteCredito = 2000;
            // Calcular el saldo total de las compras realizadas este mes
            decimal montoTotalMesActual = compras.Where(c => c.Fecha.Month == DateTime.Now.Month).Sum(c => c.Monto);

            // Calcular el saldo total de las compras realizadas el mes anterior
            decimal montoTotalMesAnterior = compras.Where(c => c.Fecha.Month == DateTime.Now.AddMonths(-1).Month).Sum(c => c.Monto);

            // Calcular el interés bonificable (25% del saldo total)
            decimal interesBonificable = saldoActual * 0.25m;

            // Calcular la cuota mínima a pagar (5% del saldo total)
            decimal cuotaMinimaPagar = saldoActual * 0.05m;

            // Calcular el monto total a pagar (saldo total)
            decimal montoTotalPagar = saldoActual;

            // Calcular el monto total de contado con intereses (saldo total más interés bonificable)
            decimal montoTotalContadoIntereses = saldoActual + interesBonificable;

            // Obtener la cuenta y el nombre asociados al estado de cuenta
            var tarjeta = await _context.Tarjetas.FirstOrDefaultAsync();

            // Obtener  datos de compra 
            var comprasdato = await _context.Compras.FirstOrDefaultAsync();


            // Crear el objeto EstadoCuenta con los datos calculados y la cuenta y nombre asociados
            var estadoCuenta = new EstadoCuenta
            {
                Titular = tarjeta.Nombre + " " + tarjeta.Apellido,
                NumeroTarjeta = tarjeta.Cuenta,
                SaldoActual = saldoActual,
                LimiteCredito = limiteCredito,
                MontoTotalMesActual = montoTotalMesActual,
                MontoTotalMesAnterior = montoTotalMesAnterior,
                InteresBonificable = interesBonificable,
                CuotaMinimaPagar = cuotaMinimaPagar,
                MontoTotalPagar = montoTotalPagar,
                MontoTotalContadoIntereses = montoTotalContadoIntereses,
                Fecha = comprasdato.Fecha,
                Descripcion = comprasdato.Descripcion,
                Monto = comprasdato.Monto
            };

            // Agregar la cuenta y el nombre al objeto devuelto
            var estadoCuentaResponse = new
            {
                estadoCuenta.Titular,
                estadoCuenta.NumeroTarjeta,
                estadoCuenta.SaldoActual,
                estadoCuenta.LimiteCredito,
                estadoCuenta.MontoTotalMesActual,
                estadoCuenta.MontoTotalMesAnterior,
                estadoCuenta.InteresBonificable,
                estadoCuenta.CuotaMinimaPagar,
                estadoCuenta.MontoTotalPagar,
                estadoCuenta.MontoTotalContadoIntereses,
                estadoCuenta.Fecha,
                estadoCuenta.Descripcion,
                estadoCuenta.Monto
            };

            return Ok();
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using TarjetaBA.Cliente.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Linq;
using AutoMapper;


namespace TarjetaBA.Cliente.Controllers
{//Se crea una el http
    public class TarjetaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        public TarjetaController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient();
            //Se conecta a la api asegurse en conectarse
            _httpClient.BaseAddress = new Uri("https://localhost:7095/api");
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/TarjetasCreditos/Lista");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                //Se renderaliza la data
                var tarjetasDelDominio = JsonConvert.DeserializeObject<IEnumerable<TarjetaViewModel>>(content);
                //Se mapea la tarjeta
                var tarjetasViewModel = _mapper.Map<IEnumerable<TarjetaViewModel>>(tarjetasDelDominio);
                return View("Index", tarjetasViewModel);
            }
            return View(new List<TarjetaViewModel>());

        }
        public IActionResult Create()
        {
            return View();
        }
        // se agrega metodo para agregar tarjeta
        [HttpPost]
        public async Task<IActionResult> Create(TarjetaViewModel tarjeta)
        {
            if (ModelState.IsValid)
            {
                var tarjetaDominio = _mapper.Map<TarjetaViewModel>(tarjeta);
                var json = JsonConvert.SerializeObject(tarjetaDominio);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/TarjetasCreditos/crear", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear la tarjeta.");
                }
            }
            return View(tarjeta);
        }
        //Se tenia planeado para editar tarjeta
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/TarjetasCreditos/Ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tarjetaDominio = JsonConvert.DeserializeObject<TarjetaViewModel>(content);
                var tarjetaViewModel = _mapper.Map<TarjetaViewModel>(tarjetaDominio);

                // Devuelve la vista de edición con los detalles de la tarjeta.
                return View(tarjetaViewModel);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles de la tarjeta.
                return RedirectToAction("Detalle"); // Redirige a la página de lista de tarjetas u otra acción apropiada.
            }
        }

        //Se tenia planeado para editar tarjeta
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TarjetaViewModel tarjeta)
        {
            if (ModelState.IsValid)
            {
                var tarjetaDominio = _mapper.Map<TarjetaViewModel>(tarjeta);
                var json = JsonConvert.SerializeObject(tarjetaDominio);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/TarjetasCreditos/editar?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de actualización exitosa, por ejemplo, redirigiendo a la página de detalles de la tarjeta.
                    return RedirectToAction("Detalle", new { id });
                }
                else
                {
                    // Manejar el caso de error en la solicitud PUT o POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al actualizar la tarjeta.");
                }
            }

            // Si hay errores de validación, vuelve a mostrar el formulario de edición con los errores.
            return View(tarjeta);
        }
        //Se tenia planeado para ver detalle de tarjeta
        public async Task<IActionResult> Detalle(int id)
        {
            var response = await _httpClient.GetAsync($"/api/TarjetasCreditos/Ver?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tarjetaDominio = JsonConvert.DeserializeObject<TarjetaViewModel>(content);
                var tarjetaViewModel = _mapper.Map<TarjetaViewModel>(tarjetaDominio);

                // Devuelve la vista de detalle con los detalles de la tarjeta.
                return View(tarjetaViewModel);
            }
            else
            {
                // Manejar el caso de error al obtener los detalles de la tarjeta.
                return RedirectToAction("Index"); // Redirige a la página de lista de tarjetas u otra acción apropiada.
            }
        }

        // Se tenia planeado para eliminar una tarjeta
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/TarjetasCreditos/eliminar?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Maneja el caso de eliminación exitosa, por ejemplo, redirigiendo a la página de lista de tarjetas.
                return RedirectToAction("Index");
            }
            else
            {
                // Maneja el caso de error en la solicitud DELETE, por ejemplo, mostrando un mensaje de error.
                TempData["Error"] = "Error al eliminar la tarjeta.";
                return RedirectToAction("Index");
            }
        }

        // Método para mostrar la vista de compra
        public IActionResult Compra()
        {
            return View();
        }

        // parcialmenta para compra no funciona debido a tablas relacionales
        [HttpPost]
        public async Task<IActionResult> Compra(CompraViewModel compra)
        {
            if (ModelState.IsValid)
            {
                //Se mapea compra
                var compraDominio = _mapper.Map<CompraViewModel>(compra);
                var json = JsonConvert.SerializeObject(compraDominio);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Compras/crear", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear la compra.");
                }
            }
            return View(compra);
        }

        // Método para mostrar la vista de pago
        public IActionResult Pago()
        {
            return View();
        }

        // Método para realizar un pago
        [HttpPost]
        // parcialmenta para compra no funciona debido a tablas relacionales
        public async Task<IActionResult> Pago(PagoViewModel pago)
        {
            if (ModelState.IsValid)
            {
                //Se mapea Pago
                var pagoDominio = _mapper.Map<PagoViewModel>(pago);
                var json = JsonConvert.SerializeObject(pagoDominio);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Pagos/crear", content);

                if (response.IsSuccessStatusCode)
                {
                    // Manejar el caso de creación exitosa.
                    return RedirectToAction("Index");
                }
                else
                {
                    // Manejar el caso de error en la solicitud POST, por ejemplo, mostrando un mensaje de error.
                    ModelState.AddModelError(string.Empty, "Error al crear el pago.");
                }
            }
            return View(pago);
        }

        // Método para mostrar el estado de cuenta
        //Se tenia planeado mostra dato y tarjeta
        public async Task<IActionResult> EstadoCuenta(int id)
        {
            try
            {
                // Realizar la solicitud HTTP para obtener el estado de cuenta
                HttpResponseMessage estadoCuentaResponse = await _httpClient.GetAsync($"/api/EstadoCuenta?id={id}");

                // Realizar la solicitud HTTP para obtener las compras
                HttpResponseMessage comprasResponse = await _httpClient.GetAsync($"/api/Compras/ListaCompra?id={id}");

                // Verificar si ambas solicitudes fueron exitosas
                if (estadoCuentaResponse.IsSuccessStatusCode && comprasResponse.IsSuccessStatusCode)
                {
                    // Leer el contenido de las respuestas
                    string estadoCuentaContent = await estadoCuentaResponse.Content.ReadAsStringAsync();
                    string comprasContent = await comprasResponse.Content.ReadAsStringAsync();

                    // Deserializar los contenidos JSON en objetos C#
                    EstadoCuentaViewModel estadoCuenta = JsonConvert.DeserializeObject<EstadoCuentaViewModel>(estadoCuentaContent);
                    List<CompraViewModel> compras = JsonConvert.DeserializeObject<List<CompraViewModel>>(comprasContent);

                    // Verificar si la lista de compras es null y, si es así, inicializarla como una lista vacía
                    estadoCuenta.Compras = compras ?? new List<CompraViewModel>();

                    // Devolver la vista con el estado de cuenta
                    return View(estadoCuenta);
                }
                else
                {
                    // Manejar el caso de error al obtener los detalles del estado de cuenta o las compras.
                    // Por ejemplo, puedes redirigir a una página de error o mostrar un mensaje de error al usuario
                    ViewBag.ErrorMessage = "Error al obtener los detalles del estado de cuenta o las compras.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción, por ejemplo, mostrando un mensaje de error
                ViewBag.ErrorMessage = "Error al procesar la solicitud: " + ex.Message;
                return View("Error");
            }
        }

        // Método para mostrar la transacción
        //Se tiene planeado mostrar compra y pago
        public async Task<IActionResult> Transaccion(int id)
        {
            var transaccionResponse = await _httpClient.GetAsync($"/api/Transaccion?id={id}");
            var pagosResponse = await _httpClient.GetAsync($"/api/Pagos/ListaPago?id={id}");
            var comprasResponse = await _httpClient.GetAsync($"/api/Compras/ListaCompra?id={id}");

            if (transaccionResponse.IsSuccessStatusCode && pagosResponse.IsSuccessStatusCode)
            {
                var transaccionContent = await transaccionResponse.Content.ReadAsStringAsync();
                var pagosContent = await pagosResponse.Content.ReadAsStringAsync();
                var comprasContent = await comprasResponse.Content.ReadAsStringAsync();

                var transaccion = JsonConvert.DeserializeObject<TransaccionViewModel>(transaccionContent);
                var pagos = JsonConvert.DeserializeObject<List<PagoViewModel>>(pagosContent);
                var compras = JsonConvert.DeserializeObject<List<CompraViewModel>>(comprasContent);
                transaccion.Pagos = pagos;
                transaccion.Compras = compras;

                return View(transaccion);
            }
            else
            {
                // Se maneja el caso de error al obtener los detalles del estado cuenta o las compras.
                return RedirectToAction("Error", "Home"); // Redirige a una página de error.
            }
        }

        // Método para exportar a Excel
        public IActionResult ExportarExcel(EstadoCuentaViewModel estadoCuenta)
        {
            // Crear archivo Excel
            using (var package = new ExcelPackage())
            {
                // Agregar hoja de trabajo
                var worksheet = package.Workbook.Worksheets.Add("Transaccion");

                // Configurar cabeceras de columnas
                worksheet.Cells[1, 1].Value = "Fecha";
                worksheet.Cells[1, 2].Value = "Descripción";
                worksheet.Cells[1, 3].Value = "Monto";

                // Agregar datos de las compras
                var row = 2;
                foreach (var compra in estadoCuenta.Compras)
                {
                    worksheet.Cells[row, 1].Value = compra.Fecha;
                    worksheet.Cells[row, 2].Value = compra.Descripcion;
                    worksheet.Cells[row, 3].Value = compra.Monto;
                    row++;
                }

                // Guardar el archivo Excel en un stream de memoria
                var stream = new MemoryStream(package.GetAsByteArray());

                // Devolver el archivo Excel como un archivo para descargar
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EstadoCuenta.xlsx");
            }
        }
    }

    }

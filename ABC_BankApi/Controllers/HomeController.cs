using ABC_BankApi.Model;
using ABC_BankApi.Models;
using ABC_BankApi.Servicios;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace ABC_BankApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_Api _servicio_Api;

        public HomeController(IServicio_Api servicio_Api)
        {
            _servicio_Api = servicio_Api;
        }

        public async Task<IActionResult> Index()
        {
            List<Contacto> Lista = await _servicio_Api.Lista();

            return View(Lista);
        }

        public async Task<IActionResult> Contacto(int id)
        {
            Contacto modelo_Contacto = new Contacto();

            ViewBag.Accion = "Nuevo contacto";
            if (id != 0)
            {
                modelo_Contacto = await _servicio_Api.Obtener(id);
                ViewBag.Accion = "Editar contacto";
            }
            List<Contacto> Lista = await _servicio_Api.Lista();

            return View(modelo_Contacto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Contacto ob_contacto)
        {
            bool respuesta;
            if (ob_contacto.Id == 0)
            {
                respuesta = await _servicio_Api.Guardar(ob_contacto);
            }
            else
            {
                respuesta = await _servicio_Api.Editar(ob_contacto);
            }

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var respuesta = await _servicio_Api.Eliminar(id);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
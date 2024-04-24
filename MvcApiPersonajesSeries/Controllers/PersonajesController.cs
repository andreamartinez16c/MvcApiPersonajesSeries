using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesSeries.Models;
using MvcApiPersonajesSeries.Services;

namespace MvcApiPersonajesSeries.Controllers
{
    public class PersonajesController : Controller
    {
        private ServicePersonajes service;
        public PersonajesController(ServicePersonajes service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> Details(int id)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(id);
            return View(personaje);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.InsertPersonaje(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(id);
            return View(personaje);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.UpdatePersonaje(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonaje(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Buscador()
        {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buscador(string serie)
        {
            ViewData["SERIES"] = await this.service.GetSeriesAsync();
            List<Personaje> personajes = await this.service.GetPersonajesBySerieAsync(serie);
            return View(personajes);
        }
    }
}

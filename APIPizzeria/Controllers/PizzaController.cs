using APIPizzeria.DataBase;
using Microsoft.AspNetCore.Mvc;
using APIPizzeria.Models;
using System.Collections.Generic;
using APIPizzeria.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIPizzeria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {

        private readonly PizzaService _pizzaService;

        public PizzaController(PizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult GetAllPizzas() 
        {
            var lista=  _pizzaService.ObtenerPizzas();
            return Ok(lista);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetPizzaByID(int id)
        {
            var pizza = _pizzaService.ObtenerPizzaPorID(id);
            if (pizza == null) 
            {
                return NotFound();
            }
            return Ok(pizza);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> crearPizza ([FromForm] Pizza pizza, IFormFile? imagen)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string? rutaImagen = null;

            try
            {
                //Llamo al método para guardar la imágne
                rutaImagen= await GuardarImagen(imagen);
                //Si me devuelve el nombre lo asigno, sino seteo null
                pizza.Imagen = rutaImagen ?? "";
                // Crear la pizza en la base de datos
                _pizzaService.CrearPizza(pizza);

                return Ok("Pizza creada correctamente");
            }
            catch
            {
                // si captura el error también borro la imagen insertada
                if (!string.IsNullOrEmpty(rutaImagen))
                {
                    string rutaFisica = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        rutaImagen.TrimStart('/')
                    );

                    if (System.IO.File.Exists(rutaFisica))
                        System.IO.File.Delete(rutaFisica);
                }
                    return StatusCode(500, "Ocurrió un error al crear la pizza.");
            }
        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPizza(int id, [FromForm] Pizza pizza, IFormFile? imagen) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var pizzaExistente = _pizzaService.ObtenerPizzaPorID(id);
            if (pizzaExistente==null) return NotFound();
            else
            {
                try
                {

                    if (!string.IsNullOrEmpty(pizzaExistente.Imagen))
                    {
                        string rutaVieja = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", pizzaExistente.Imagen.TrimStart('/'));
                        if (System.IO.File.Exists(rutaVieja)) System.IO.File.Delete(rutaVieja);
                    }

                    // Coloca la nueva imágen 
                    string? nuevaRuta = await GuardarImagen(imagen);
                    pizza.Imagen = nuevaRuta ?? pizzaExistente.Imagen;
                }
                catch (Exception ex)
                {
                 throw new Exception (ex.Message);
                }
            }

            _pizzaService.ActualizarPizza(pizza,id);
            return Ok("Pizza actualizada");        
        }
     
        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var pizza = _pizzaService.ObtenerPizzaPorID(id);
            if (pizza == null) return NotFound();

            _pizzaService.EliminarPizza(id);
            return Ok("Pizza eliminada");
        }


        #region Metodos
        private async Task<string?> GuardarImagen (IFormFile? imagen) 
        {
            // Metodo creado para reutilizar código al gaurdar imágen

            if (imagen == null || imagen.Length == 0) return null;
            //Valido que sea un archivo de tipo imágen
            var extensionesValidas = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(imagen.FileName).ToLower();
            if (!extensionesValidas.Contains(extension)) { throw new Exception("Formato del archivo no válido"); }

            string rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(rutaCarpeta)) Directory.CreateDirectory(rutaCarpeta);

            // asigno un valor unico
            string nombreUnico = $"{Guid.NewGuid()}{extension}";
            string rutaCompleta = Path.Combine(rutaCarpeta, nombreUnico);

            // guardp
            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await imagen.CopyToAsync(stream);
            }

            return "/images/" + nombreUnico;
        }
        #endregion
    }
}

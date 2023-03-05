using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020MS650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020MS650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly restauranteContext _restauranteDBContexto;

        public PlatosController(restauranteContext restauranteContexto)
        {
            _restauranteDBContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<platos> listadoPlatos = (from e in _restauranteDBContexto.platos select e).ToList();

            if (listadoPlatos.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPlatos);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            platos? plato = (from e in _restauranteDBContexto.platos
                               where e.platoId == id
                               select e).FirstOrDefault();

            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);
        }


        [HttpGet]
        [Route("Add")]
        public IActionResult Guardarplato([FromBody] platos plato)
        {
            try
            {
                _restauranteDBContexto.platos.Add(plato);
                _restauranteDBContexto.SaveChanges();
                return Ok(plato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPlatos(int id, [FromBody] platos platoModificar)
        {
            platos? platoActual = (from e in _restauranteDBContexto.platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();

            if (platoActual == null)
            {
                return NotFound();
            }

            platoActual.platoId = platoModificar.platoId;
            platoActual.precio = platoModificar.precio;

            _restauranteDBContexto.Entry(platoActual).State = EntityState.Modified;
            _restauranteDBContexto.SaveChanges();

            return Ok(platoModificar);
        }


        [HttpGet]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarplato(int id)
        {
            platos? plato = (from e in _restauranteDBContexto.platos
                               where e.platoId == id
                               select e).FirstOrDefault();

            if (plato == null)
            {
                return NotFound();
            }

            _restauranteDBContexto.platos.Attach(plato);
            _restauranteDBContexto.platos.Remove(plato);
            _restauranteDBContexto.SaveChanges();

            return Ok(plato);
        }


        //Filtrar por palabra que contenga un plato

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FiltrarxNombre(string filtro)
        {
            platos? plato = (from e in _restauranteDBContexto.platos
                             where e.nombrePlato.Contains(filtro)
                             select e).FirstOrDefault();

            if (plato == null)
            {
                return NotFound();
            }
            return Ok(plato);
        }
    }
}

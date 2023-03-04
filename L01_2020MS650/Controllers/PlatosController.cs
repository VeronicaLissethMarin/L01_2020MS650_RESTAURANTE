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
            List<pedidos> listadoEquipo = (from e in _restauranteDBContexto.pedidos select e).ToList();

            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            pedidos? pedido = (from e in _restauranteDBContexto.pedidos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            pedidos? pedido = (from e in _restauranteDBContexto.pedidos
                               where e.descripcion.Contains(filtro)
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] pedidos pedido)
        {
            try
            {
                _restauranteDBContexto.pedidos.Add(pedido);
                _restauranteDBContexto.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody] pedidos equipoModificar)
        {
            pedidos? equipoActual = (from e in _restauranteDBContexto.pedidos
                                     where e.id_equipos == id
                                     select e).FirstOrDefault();

            if (equipoActual == null)
            {
                return NotFound();
            }

            equipoActual.nombre = equipoModificar.nombre;
            equipoActual.descripcion = equipoModificar.descripcion;
            equipoActual.marca_id = equipoModificar.marca_id;
            equipoActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
            equipoActual.anio_compra = equipoModificar.anio_compra;
            equipoActual.costo = equipoModificar.costo;

            _restauranteDBContexto.Entry(equipoActual).State = EntityState.Modified;
            _restauranteDBContexto.SaveChanges();

            return Ok(equipoModificar);
        }


        [HttpGet]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            pedidos? equipo = (from e in _restauranteDBContexto.pedidos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }

            _restauranteDBContexto.pedidos.Attach(equipo);
            _restauranteDBContexto.pedidos.Remove(equipo);
            _restauranteDBContexto.SaveChanges();

            return Ok(equipo);
        }
    }
}

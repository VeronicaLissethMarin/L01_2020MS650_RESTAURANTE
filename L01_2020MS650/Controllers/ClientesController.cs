using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020MS650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020MS650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly restauranteContext _restauranteDBContexto;

        public ClientesController(restauranteContext restauranteContexto)
        {
            _restauranteDBContexto = restauranteContexto;
        }

       [HttpGet]
       [Route("GetAll")]
        public IActionResult Get()
        {
           List<clientes> listadocliente = (from e in _restauranteDBContexto.clientes select e).ToList();

            if (listadocliente.Count() == 0)
           {
               return NotFound();
            }
           return Ok(listadocliente);
       }


        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            clientes? clientes = (from e in _restauranteDBContexto.clientes
                               where e.clienteId == id
                               select e).FirstOrDefault();

            if (clientes == null)
            {
                return NotFound();
            }
            return Ok(clientes);
        }


        [HttpGet]
        [Route("Add")]
        public IActionResult Guardarcliente([FromBody] clientes cliente)
        {
            try
            {
                _restauranteDBContexto.clientes.Add(cliente);
                _restauranteDBContexto.SaveChanges();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("actualizar/{id}")]
        public IActionResult Actualizarcliente(int id, [FromBody] clientes clienteModificar)
        {
            clientes? clienteActual = (from e in _restauranteDBContexto.clientes
                                     where e.clienteId == id
                                     select e).FirstOrDefault();

            if (clienteActual == null)
            {
                return NotFound();
            }

            clienteActual.nombreCliente = clienteModificar.nombreCliente;
            clienteActual.direccion = clienteModificar.direccion;

            _restauranteDBContexto.Entry(clienteActual).State = EntityState.Modified;
            _restauranteDBContexto.SaveChanges();

            return Ok(clienteModificar);
        }


        [HttpGet]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarcliente(int id)
        {
            clientes? cliente = (from e in _restauranteDBContexto.clientes
                               where e.clienteId == id
                               select e).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound();
            }

            _restauranteDBContexto.clientes.Attach(cliente);
            _restauranteDBContexto.clientes.Remove(cliente);
            _restauranteDBContexto.SaveChanges();

            return Ok(cliente);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult BuscarxDireccion(string filtro)
        {
            clientes? cliente = (from e in _restauranteDBContexto.clientes
                                  where e.direccion.Contains(filtro)
                                  select e).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
    }
}

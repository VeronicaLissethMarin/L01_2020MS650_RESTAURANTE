using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020MS650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020MS650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {

        private readonly restauranteContext _restauranteDBContexto;

        public PedidosController(restauranteContext restauranteContexto)
        {
            _restauranteDBContexto = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<pedidos> listadoPedido = (from e in _restauranteDBContexto.pedidos select e).ToList();

            if (listadoPedido.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoPedido);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            pedidos? pedido = (from e in _restauranteDBContexto.pedidos
                               where e.pedidoId == id
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
        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidoModificar)
        {
            pedidos? pedidoActual = (from e in _restauranteDBContexto.pedidos
                                     where e.pedidoId == id
                                     select e).FirstOrDefault();

            if (pedidoActual == null)
            {
                return NotFound();
            }

            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            _restauranteDBContexto.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteDBContexto.SaveChanges();

            return Ok(pedidoModificar);
        }


        [HttpGet]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            pedidos? pedido = (from e in _restauranteDBContexto.pedidos
                               where e.pedidoId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            _restauranteDBContexto.pedidos.Attach(pedido);
            _restauranteDBContexto.pedidos.Remove(pedido);
            _restauranteDBContexto.SaveChanges();

            return Ok(pedido);
        }

        //Filtrar por clienteid

        [HttpGet]
        [Route("Find/{filtroC}")]

       
        public IActionResult FiltrarxCLiente(int filtroC)
        {
            pedidos? pedido = (from e in _restauranteDBContexto.pedidos
                               where e.clienteId==filtroC
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        //flitrado por motorista

        [HttpGet]
        [Route("Find/{filtroM}")]

        public IActionResult FiltrarxMotorista(int filtroM)
        {
            pedidos? pedido = (from e in _restauranteDBContexto.pedidos
                               where e.motoristaId == filtroM
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
    }
}

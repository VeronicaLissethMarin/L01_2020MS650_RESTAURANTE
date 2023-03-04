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

        
    }
}

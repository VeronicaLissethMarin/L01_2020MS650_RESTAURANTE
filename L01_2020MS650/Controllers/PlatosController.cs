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
    }
}

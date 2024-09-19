using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebAPICrudTest.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPICrudTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private readonly DbcrudContext dbContext;

        public EmpleadoController(DbcrudContext _dbcrudContext)
        {
            dbContext = _dbcrudContext;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Get()
        {
            var listaEmpleado = await dbContext.Empleados.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaEmpleado);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Empleado E)
        {
            try
            {
                // Código que podría lanzar una excepción
                await dbContext.Empleados.AddAsync(E);
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine(ex.Message);  // Muestra el mensaje de error
            }
        }
    }
}

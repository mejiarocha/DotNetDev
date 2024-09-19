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
        [Route("list")]
        public async Task<IActionResult> Get()
        {
            var listaEmpleado = await dbContext.Empleados.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaEmpleado);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Empleado E)
        {
            try
            {
                // Código que podría lanzar una excepción
                await dbContext.Empleados.AddAsync(E);
                await dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, E);
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                Console.WriteLine(ex.Message);  
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var empleado = await dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
                return StatusCode(StatusCodes.Status200OK, empleado);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit([FromBody] Empleado E)
        {
            try
            {
                dbContext.Empleados.Update(E);
                await dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, E);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var empleado = await dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
                dbContext.Empleados.Remove(empleado);
                await dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

using Jiran.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jiran.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {
        
        private readonly JiranAppContext _dbContext;

        public SystemController(JiranAppContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("GetSystem")]
        public async Task<IActionResult> GetSystem()
        {

            List<MasterSystem> systemList = await _dbContext.MasterSystems.ToListAsync();

            if (systemList != null && systemList.Count > 0) return Ok(systemList);
            else return BadRequest("No system Found");
        }





    }
}

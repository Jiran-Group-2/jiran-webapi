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
        public async Task<IActionResult> GetSystem(int providedSystemID)
        {

            List<MasterSystem> systemList = await _dbContext.MasterSystems.Where(u => u.SystemId == providedSystemID).ToListAsync();

            if (systemList != null && systemList.Count > 0) return Ok(systemList);
            else return BadRequest("No system Found");
        }

        [HttpPost]
        [Route("AddSystem")]
        public async Task<IActionResult> AddSystem(string providedVersion, string providedAreaName, string providedAddress, string providedOfficeNo1,string providedOfficeNo2,
        string providedFax, string providedEmail)
        {
            //DateTime providedCreatedDate = DateTime.Now;


            using (var dbContext = new JiranAppContext())
            {
                var newSystem = new MasterSystem
                {
                    Version = providedVersion,
                    AreaName = providedAreaName,
                    Address = providedAddress,
                    OfficeNumber1 = providedOfficeNo1,
                    OfficeNumber2 = providedOfficeNo2,
                    Fax = providedFax,
                    Email = providedEmail
                };

                dbContext.MasterSystems.Add(newSystem);
                dbContext.SaveChanges();
            }


            return Ok();
        }

        [HttpPost]
        [Route("UpdateSystem")]
        public async Task<IActionResult> UpdateSystem(int providedSystemID, string providedVersion, string providedAreaName, string providedAddress, string providedOfficeNo1,string providedOfficeNo2,
        string providedFax, string providedEmail)
        {
            var systemToUpdate = _dbContext.MasterSystems.FirstOrDefault(u => u.SystemId == providedSystemID);

            //int userID = userToUpdate.UserId;
            // If the user is found, update its properties
            if (systemToUpdate != null)
            {

                systemToUpdate.Version  = providedVersion == null ? systemToUpdate.Version: providedVersion;
                systemToUpdate.AreaName  = providedAreaName == null ? systemToUpdate.AreaName: providedAreaName;
                systemToUpdate.Address = providedAddress == null ? systemToUpdate.Address: providedAddress;
                systemToUpdate.OfficeNumber1 = providedOfficeNo1 == null ? systemToUpdate.OfficeNumber1: providedOfficeNo1;
                systemToUpdate.OfficeNumber2  = providedOfficeNo2 == null ? systemToUpdate.OfficeNumber2: providedOfficeNo2;
                systemToUpdate.Fax = providedFax == null ? systemToUpdate.Fax: providedFax;
                systemToUpdate.Email  = providedEmail == null ? systemToUpdate.Email: providedEmail;

                // Save changes to persist the updates
                _dbContext.SaveChanges();
            }


            return Ok();
        }


    }
}

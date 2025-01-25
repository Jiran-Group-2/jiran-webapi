using Jiran.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jiran.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly JiranAppContext _dbContext;
        public AnnouncementController(JiranAppContext _context)
        {
            _dbContext = _context;
        }




        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int systemID)
        {
            List<MasterAnnouncement> announcementList = await _dbContext.MasterAnnouncements.Include(u=> u.System).Where(u => u.SystemId == systemID).ToListAsync();
            //test pushing

            return Ok(announcementList);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(string providedAnnouncementSubject, string providedAnnouncementDescription, int providedCreatedById, int providedSystemID)
        {
            //DateTime providedCreatedDate = DateTime.Now;
            // Get the Singapore Standard Time zone (used by Malaysia)
            TimeZoneInfo malaysiaZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

            // Get the current time in UTC
            DateTime utcTime = DateTime.UtcNow;

            // Convert the current UTC time to Malaysia Time
            DateTime providedCreatedDate = TimeZoneInfo.ConvertTimeFromUtc(utcTime, malaysiaZone);

            var annoToUpdate = _dbContext.MasterAnnouncements.FirstOrDefault(u => u.AnnouncementSubject == providedAnnouncementSubject);

            if (annoToUpdate != null) { return BadRequest("There has already exist announcement with the same subject!"); }

            using (var dbContext = new JiranAppContext())
            {
                var newAnnountment = new MasterAnnouncement
                {
                    AnnouncementSubject = providedAnnouncementSubject,
                    AnnouncementDescription = providedAnnouncementDescription,
                    CreatedById = providedCreatedById,
                    CreatedDate = providedCreatedDate,
                    SystemId = providedSystemID
                };

                dbContext.MasterAnnouncements.Add(newAnnountment);
                dbContext.SaveChanges();
            }


            List<MasterAnnouncement> announcementList = await _dbContext.MasterAnnouncements.Where(u => u.AnnouncementSubject == providedAnnouncementSubject).ToListAsync();


            return Ok(announcementList);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int announcementID)
        {
            var annoToDelete = _dbContext.MasterAnnouncements.Find(announcementID); // Assuming blockId is the ID of the block to delete

            // If the block is found, remove it
            if (annoToDelete != null)
            {
                _dbContext.MasterAnnouncements.Remove(annoToDelete);

                // Save changes to persist the deletion
                _dbContext.SaveChanges();

                return Ok(); // or return some other response indicating success
            }
            else
            {
                return NotFound(); // or return some other response indicating that the block was not found
            }

        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(int announcementID, string providedAnnouncementSubject, string providedAnnouncementDescription)
        {
            var annoToUpdate = _dbContext.MasterAnnouncements.FirstOrDefault(u => u.AnnouncementId == announcementID);

            // If the user is found, update its properties
            if (annoToUpdate != null)
            {
                annoToUpdate.AnnouncementSubject = providedAnnouncementSubject;
                annoToUpdate.AnnouncementDescription = providedAnnouncementDescription;

                // Save changes to persist the updates
                _dbContext.SaveChanges();
            }
            else{
                return BadRequest("Not exist");
            }


            return Ok();
        }


    }
}

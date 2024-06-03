using Jiran.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jiran.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComplaintController : ControllerBase
    {
        private readonly JiranAppContext _dbContext;

        public ComplaintController(JiranAppContext _context)
        {
            _dbContext = _context;
        }


        [HttpGet]
        [Route("GetComplaintCategory")]
        public async Task<IActionResult> GetComplaintCategory()
        {

            List<MasterComplaintCategory> complaintCategoryList = await _dbContext.MasterComplaintCategories.ToListAsync();

            if (complaintCategoryList != null && complaintCategoryList.Count > 0) return Ok(complaintCategoryList);
            else return BadRequest("No complaint category Found");
        }

        [HttpGet]
        [Route("GetComplaint")]
        public async Task<IActionResult> GetComplaint(int systemID, int userID)
        {
            List<MasterComplaint> complaintCategoryList = null;
            if(systemID > 0 && userID == 0) 
                {complaintCategoryList = await _dbContext.MasterComplaints.Include(u => u.complaintCategory).Where(u => u.SystemId == systemID).ToListAsync();}
            else if (userID > 0 && systemID == 0)
                {complaintCategoryList = await _dbContext.MasterComplaints.Include(u => u.complaintCategory).Where(u => u.UserId == userID).ToListAsync();}
            else
                { complaintCategoryList = await _dbContext.MasterComplaints.Include(u => u.complaintCategory).Where(u => u.UserId == userID).ToListAsync();}

            if (complaintCategoryList != null && complaintCategoryList.Count > 0) return Ok(complaintCategoryList);
            else return BadRequest("No complaint Found");
        }

        
        [HttpPost]
        [Route("AddComplaint")]
        public async Task<IActionResult> AddComplaint(int providedUserID, int providedComplaintCategoryID, string providedComplaintLocation,
        string providedComplaintSubject, string providedComplaintDescription, int providedSystemID)
        {
            //DateTime providedCreatedDate = DateTime.Now;
            // Get the Singapore Standard Time zone (used by Malaysia)
            TimeZoneInfo malaysiaZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

            // Get the current time in UTC
            DateTime utcTime = DateTime.UtcNow;

            // Convert the current UTC time to Malaysia Time
            DateTime providedCreatedDate = TimeZoneInfo.ConvertTimeFromUtc(utcTime, malaysiaZone);


            using (var dbContext = new JiranAppContext())
            {
                var newComplaint = new MasterComplaint
                {
                    UserId = providedUserID,
                    CreatedDate = providedCreatedDate,
                    ComplaintCategoryId = providedComplaintCategoryID,
                    ComplaintLocation = providedComplaintLocation,
                    ComplaintSubject = providedComplaintSubject,
                    ComplaintDescription = providedComplaintDescription,
                    AttachmentId = 0,
                    FeedbackId = 0,
                    Status = "P",
                    SystemId = providedSystemID
                };

                dbContext.MasterComplaints.Add(newComplaint);
                dbContext.SaveChanges();
            }


            return Ok();
        }

        [HttpPost]
        [Route("UpdateComplaint")]
        public async Task<IActionResult> UpdateComplaint(int providedComplaintID, int providedComplaintCategoryID, string providedComplaintLocation,
        string providedComplaintSubject, string providedComplaintDescription, int providedAttachmentID, int providedFeedbackID, string providedStatus)
        {
            var complaintToUpdate = _dbContext.MasterComplaints.FirstOrDefault(u => u.ComplaintId == providedComplaintID);

            //int userID = userToUpdate.UserId;
            // If the user is found, update its properties
            if (complaintToUpdate != null)
            {

                //complaintToUpdate.UserId = providedName == null ? userToUpdate.Name: providedName;
                //complaintToUpdate.CreatedDate = = providedName == null ? userToUpdate.Name: providedName;
                complaintToUpdate.ComplaintCategoryId = providedComplaintCategoryID == 0 ? complaintToUpdate.ComplaintCategoryId: providedComplaintCategoryID;
                complaintToUpdate.ComplaintLocation = providedComplaintLocation == null ? complaintToUpdate.ComplaintLocation: providedComplaintLocation;
                complaintToUpdate.ComplaintSubject = providedComplaintSubject == null ? complaintToUpdate.ComplaintSubject: providedComplaintSubject;
                complaintToUpdate.ComplaintDescription = providedComplaintDescription == null ? complaintToUpdate.ComplaintDescription: providedComplaintDescription;
                complaintToUpdate.AttachmentId= providedAttachmentID == 0 ? complaintToUpdate.AttachmentId: providedAttachmentID;
                complaintToUpdate.FeedbackId = providedFeedbackID == 0 ? complaintToUpdate.FeedbackId: providedFeedbackID;
                complaintToUpdate.Status= providedStatus == null ? complaintToUpdate.Status: providedStatus;
                //complaintToUpdate.SystemId = providedName == null ? complaintToUpdate.Name: providedName;



                // Save changes to persist the updates
                _dbContext.SaveChanges();
            }


            return Ok();
        }

    }
}
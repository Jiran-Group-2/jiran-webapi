using Jiran.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jiran.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly JiranAppContext _dbContext;

        public UserController(JiranAppContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> login(string username, string password)
        {

            List<MasterUser> userList = await _dbContext.MasterUsers.Include(u => u.Role).Include(u => u.System)
            .Include(u => u.UnitNumber)
               .ThenInclude(u => u.Floor)
               .ThenInclude(u => u.Block)
            .Where(u => u.UserLogin == username && u.Password == password && u.Status == "A").ToListAsync();

            if (userList != null && userList.Count > 0) return Ok(userList);
            else return BadRequest("No user Found");
        }

        [HttpGet]
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAllUser(int systemID)
        {

            List<MasterUser> userList = await _dbContext.MasterUsers.Include(u => u.Role).Include(u => u.System).Include(u => u.Title)
            .Include(u => u.UnitNumber)
               .ThenInclude(u => u.Floor)
               .ThenInclude(u => u.Block)
            .Where(u => u.SystemId == systemID).ToListAsync();

            if (userList != null && userList.Count > 0) return Ok(userList);
            else return BadRequest("No user Found");
        }

        [HttpGet]
        [Route("GetTitle")]
        public async Task<IActionResult> GetTitle()
        {

            List<MasterTitle> titleList = await _dbContext.MasterTitles.ToListAsync();

            if (titleList != null && titleList.Count > 0) return Ok(titleList);
            else return BadRequest("No Title Found");
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(string providedUserLogin, string providedPassword, string providedName, int providedTitle, string providedNric,
            int providedUnitNumberId, string providedMobileNo, string providedHomeNo, int providedRoleId, string providedUnitNo, int providedFloorID, int providedBlockID)
        {
            //DateTime providedCreatedDate = DateTime.Now;
            // Get the Singapore Standard Time zone (used by Malaysia)
            TimeZoneInfo malaysiaZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

            // Get the current time in UTC
            DateTime utcTime = DateTime.UtcNow;

            // Convert the current UTC time to Malaysia Time
            DateTime providedCreatedDate = TimeZoneInfo.ConvertTimeFromUtc(utcTime, malaysiaZone);

            var userToUpdate = _dbContext.MasterUsers.FirstOrDefault(u => u.UserLogin == providedUserLogin);

            if (userToUpdate != null) { return BadRequest("There has already exist user with the same login!"); }

            using (var dbContext = new JiranAppContext())
            {
                var newUser = new MasterUser
                {
                    UserLogin = providedUserLogin,
                    Password = providedPassword,
                    Name = providedName,
                    TitleId = providedTitle,
                    Nric = providedNric,
                    UnitNumberId = providedUnitNumberId,
                    MobileNo = providedMobileNo,
                    HomeNo = providedHomeNo,
                    Status = "P",
                    CreatedById = 1,
                    CreatedDate = providedCreatedDate,
                    RoleId = providedRoleId
                };

                dbContext.MasterUsers.Add(newUser);
                dbContext.SaveChanges();
            }


            List<MasterUser> userList = await _dbContext.MasterUsers.Include(u => u.Role).Where(u => u.UserLogin == providedUserLogin && u.Password == providedPassword).ToListAsync();

            using (var dbContext = new JiranAppContext())
            {
                var newUnit = new MasterUnit
                {
                    UserId = userList[0].UserId,
                    UnitNumber = providedUnitNo,
                    FloorId = providedFloorID,
                    BlockId = providedBlockID,
                    CreatedById = userList[0].UserId,
                    CreatedDate = providedCreatedDate
                };

                dbContext.MasterUnits.Add(newUnit);
                dbContext.SaveChanges();
            }


            return Ok(userList);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(int providedUserID, string providedUserLogin, string providedPassword, string providedName, int providedTitle, string providedNric,
            int providedUnitNumberId, string providedMobileNo, string providedHomeNo, string providedStatus)
        {
            var userToUpdate = _dbContext.MasterUsers.FirstOrDefault(u => u.UserId == providedUserID);

            // If the user is found, update its properties
            if (userToUpdate != null)
            {
                userToUpdate.Name = providedName == null ? userToUpdate.Name : providedName;
                userToUpdate.TitleId = providedTitle == 0 ? userToUpdate.TitleId : providedTitle;
                userToUpdate.Nric = providedNric == null ? userToUpdate.Nric : providedNric;
                //userToUpdate.UnitNumberId = providedUnitNumberId;
                userToUpdate.MobileNo = providedMobileNo == null ? userToUpdate.MobileNo : providedMobileNo;
                userToUpdate.HomeNo = providedHomeNo == null ? userToUpdate.HomeNo : providedHomeNo;
                userToUpdate.Status = providedStatus;

                // Save changes to persist the updates
                _dbContext.SaveChanges();
            }


            return Ok();
        }

        [HttpPost]
        [Route("RegisterVisitor")]
        public async Task<IActionResult> RegisterVisitor(string providedVisitorName, string providedVisitorMobileNo, string providedVisitorNRIC, int providedQuantity, string providedPurposeOfVisit, int providedVehicleType,
        string providedPlateNo, int providedUnitNumberID, int providedCreatedByID)
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
                var newVisitor = new MasterVisitor
                {
                    VisitorName = providedVisitorName,
                    VisitorMobileNo = providedVisitorMobileNo,
                    VisitorNRIC = providedVisitorNRIC,
                    VisitorQuantity = providedQuantity,
                    VisitorPurposeOfVisit = providedPurposeOfVisit,
                    VisitorVehicleType = providedVehicleType,
                    VisitorVehiclePlate = providedPlateNo,
                    ApprovalStatus = "P",
                    UnitNumberId = 1,
                    CreatedById = providedCreatedByID,
                    CreatedDate = providedCreatedDate,


                };

                dbContext.MasterVisitors.Add(newVisitor);
                dbContext.SaveChanges();
            }

            List<MasterVisitor> visitorList = await _dbContext.MasterVisitors.Where(u => u.VisitorName == providedVisitorName && u.VisitorMobileNo == providedVisitorMobileNo).ToListAsync();


            if (visitorList.Count > 0)
            {
                return Ok(visitorList);
            }
            else
            {
                return BadRequest("Failed to insert");
            }
        }

        [HttpPost]
        [Route("UpdateVisitor")]
        public async Task<IActionResult> UpdateVisitor(int providedVisitorID, string providedVisitorName, string providedVisitorMobileNo, string providedVisitorNRIC, int providedQuantity, string providedPurposeOfVisit, int providedVehicleType,
        string providedPlateNo, int providedUnitNumberID, int providedCreatedByID, string providedStatus)
        {
            var visitorToUpdate = _dbContext.MasterVisitors.FirstOrDefault(u => u.VisitorId == providedVisitorID);

            int? userID = visitorToUpdate.CreatedById;
            // If the user is found, update its properties
            if (visitorToUpdate != null)
            {
                visitorToUpdate.VisitorName = providedVisitorName == null ? visitorToUpdate.VisitorName : providedVisitorName;
                visitorToUpdate.VisitorMobileNo = providedVisitorMobileNo == null ? visitorToUpdate.VisitorMobileNo : providedVisitorMobileNo;
                visitorToUpdate.VisitorNRIC = providedVisitorNRIC == null ? visitorToUpdate.VisitorNRIC : providedVisitorNRIC;
                visitorToUpdate.VisitorQuantity = providedQuantity == 0 ? visitorToUpdate.VisitorQuantity : providedQuantity;
                visitorToUpdate.VisitorPurposeOfVisit = providedPurposeOfVisit == null ? visitorToUpdate.VisitorPurposeOfVisit : providedPurposeOfVisit;
                visitorToUpdate.VisitorVehicleType = providedVehicleType == 0 ? visitorToUpdate.VisitorVehicleType : providedVehicleType;
                visitorToUpdate.VisitorVehiclePlate = providedPlateNo == null ? visitorToUpdate.VisitorVehiclePlate : providedPlateNo;
                visitorToUpdate.ApprovalStatus = providedStatus;
                visitorToUpdate.UnitNumberId = providedUnitNumberID == 0 ? visitorToUpdate.UnitNumberId : providedUnitNumberID;
                //visitorToUpdate.CreatedById = providedCreatedByID == 0 ? visitorToUpdate.Name: providedName;
                //visitorToUpdate.CreatedDate = providedCreatedDate == null ? visitorToUpdate.Name: providedName;

                // Save changes to persist the updates
                _dbContext.SaveChanges();
            }
            else
            {
                return BadRequest("Failed to update visitor");
            }


            return Ok();
        }

        [HttpGet]
        [Route("GetVisitor")]
        public async Task<IActionResult> GetVisitor(int? unitUserID)
        {
            List<MasterVisitor> visitorList = new List<MasterVisitor>();
            if (unitUserID == null)
            {
                visitorList = await _dbContext.MasterVisitors.Include(u => u.UnitNumber)
                .ToListAsync();
            }
            else
            {
                visitorList = await _dbContext.MasterVisitors.Include(u => u.UnitNumber)
                .Where(u => u.VisitorId == unitUserID)
                .ToListAsync();
            }

            if (visitorList != null && visitorList.Count > 0) return Ok(visitorList);
            else return BadRequest("No visitor Found");
        }
    }
}

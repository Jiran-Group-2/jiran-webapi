using Jiran.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jiran.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class BillController : ControllerBase
    {
        private readonly JiranAppContext _dbContext;
        public BillController(JiranAppContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("GetMasterBill")]
        public async Task<IActionResult> Get()
        {
            List<MasterBill> billList = await _dbContext.MasterBills.ToListAsync();


            return Ok(billList);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(string providedBillSubject, string providedBillDescription, decimal providedBillRate, int providedCreatedById)
        {
            //DateTime providedCreatedDate = DateTime.Now;
            // Get the Singapore Standard Time zone (used by Malaysia)
            TimeZoneInfo malaysiaZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

            // Get the current time in UTC
            DateTime utcTime = DateTime.UtcNow;

            // Convert the current UTC time to Malaysia Time
            DateTime providedCreatedDate = TimeZoneInfo.ConvertTimeFromUtc(utcTime, malaysiaZone);

            var billToUpdate = _dbContext.MasterBills.FirstOrDefault(u => u.BillSubject == providedBillSubject);

            if (billToUpdate != null) { return BadRequest("There has already exist bill with the same subject!"); }

            using (var dbContext = new JiranAppContext())
            {
                var newBill = new MasterBill
                {
                    BillSubject = providedBillSubject,
                    BillDescription = providedBillDescription,
                    BillRate = providedBillRate,
                    CreatedById = providedCreatedById,
                    CreatedDate = providedCreatedDate
                };

                dbContext.MasterBills.Add(newBill);
                dbContext.SaveChanges();
            }


            List<MasterBill> billlist = await _dbContext.MasterBills.Where(u => u.BillSubject == providedBillSubject).ToListAsync();


            return Ok(billlist);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int billID)
        {
            var billToDelete = _dbContext.MasterBills.Find(billID); // Assuming blockId is the ID of the block to delete

            // If the block is found, remove it
            if (billToDelete != null)
            {
                _dbContext.MasterBills.Remove(billToDelete);

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
        public async Task<IActionResult> Update(int billID, string providedBillSubject, string providedBillDescription, decimal providedBillRate)
        {
            var billToUpdate = _dbContext.MasterBills.FirstOrDefault(u => u.BillId == billID);

            // If the user is found, update its properties
            if (billToUpdate != null)
            {
                billToUpdate.BillSubject = providedBillSubject;
                billToUpdate.BillDescription = providedBillDescription;
                billToUpdate.BillRate = providedBillRate;

                // Save changes to persist the updates
                _dbContext.SaveChanges();
            }
            else{
                return BadRequest("Not exist");
            }


            return Ok();
        }

        [HttpGet]
        [Route("GetUnitBill")]
        public async Task<IActionResult> GetUnitBill(int? UnitNumberID)
        {
            List<MasterUnitBill> UnitBillList = new List<MasterUnitBill>();
            if(UnitNumberID == null)
            {
                UnitBillList = await _dbContext.MasterUnitBills
                .Include(u => u.UnitNumber)
                .ThenInclude(u => u.Floor)
                .ThenInclude(u => u.Block)
                .Include(u => u.Bill)
                .ToListAsync();
            }
            else{
                UnitBillList = await _dbContext.MasterUnitBills
                .Include(u => u.UnitNumber)
                .ThenInclude(u => u.Floor)
                .ThenInclude(u => u.Block)
                .Include(u => u.Bill).Where(u => u.UnitNumberId == UnitNumberID)
                .ToListAsync();
            }
            


            return Ok(UnitBillList);
        }

        [HttpPost]
        [Route("AddUnitBill")]
        public async Task<IActionResult> AddUnitBill(int providedBillID, int providedUnitNumberID, int providedUserID, 
         decimal providedAmount, decimal providedPaid, decimal providedBalance, int providedCreatedById)
        {
            //DateTime providedCreatedDate = DateTime.Now;
            // Get the Singapore Standard Time zone (used by Malaysia)
            TimeZoneInfo malaysiaZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

            // Get the current time in UTC
            DateTime utcTime = DateTime.UtcNow;

            // Convert the current UTC time to Malaysia Time
            DateTime providedCreatedDate = TimeZoneInfo.ConvertTimeFromUtc(utcTime, malaysiaZone);

            int systemID = 1;

            //var billToUpdate = _dbContext.MasterUnitBills.FirstOrDefault(u => u.BillSubject == providedBillSubject);

            //if (billToUpdate != null) { return BadRequest("There has already exist bill with the same subject!"); }

            using (var dbContext = new JiranAppContext())
            {
                var newUnitBill = new MasterUnitBill
                {
                    BillId = providedBillID,
                    UnitNumberId = providedUnitNumberID,
                    UserId = providedUserID,
                    Amount = providedAmount,
                    Paid = providedPaid,
                    Balance = providedBalance,
                    CreatedById = providedCreatedById.ToString(),
                    CreatedDate = providedCreatedDate
                };

                dbContext.MasterUnitBills.Add(newUnitBill);
                dbContext.SaveChanges();
            }


            //List<MasterBill> billlist = await _dbContext.MasterBills.Where(u => u.BillSubject == providedBillSubject).ToListAsync();


            return Ok();
        }
        [HttpPost]
        [Route("UpdateUnitBill")]
        public async Task<IActionResult> UpdateUnitBill(int UnitBillID, decimal providedPaid)
        {
            var unitBillToUpdate = _dbContext.MasterUnitBills.FirstOrDefault(u => u.UserBillId == UnitBillID);
            decimal providedAmount = unitBillToUpdate?.Amount ?? 0;
            decimal overPay = providedAmount - providedPaid;

            if (overPay <= 0)
            {return BadRequest("Over pay is not allowed");}


            // If the user is found, update its properties
            if (unitBillToUpdate != null)
            {
                unitBillToUpdate.Amount = providedAmount;
                unitBillToUpdate.Paid = providedPaid;
                unitBillToUpdate.Balance = overPay;

                // Save changes to persist the updates
                _dbContext.SaveChanges();
            }
            else{
                return BadRequest("Not exist");
            }


            return Ok();
        }

        [HttpPost]
        [Route("DeleteUnitBill")]
        public async Task<IActionResult> DeleteUnitBill(int unitBillID)
        {
            var billToDelete = _dbContext.MasterUnitBills.Find(unitBillID); // Assuming blockId is the ID of the block to delete

            // If the block is found, remove it
            if (billToDelete != null)
            {
                _dbContext.MasterUnitBills.Remove(billToDelete);

                // Save changes to persist the deletion
                _dbContext.SaveChanges();

                return Ok(); // or return some other response indicating success
            }
            else
            {
                return NotFound(); // or return some other response indicating that the block was not found
            }

        }

    }
}
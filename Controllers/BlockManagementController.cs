using Jiran.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jiran.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class BlockManagementController : ControllerBase
    {
        private readonly JiranAppContext _dbContext;
        public BlockManagementController(JiranAppContext _context)
        {
            _dbContext = _context;
        }

        [HttpGet]
        [Route("GetBlockList")]
        public async Task<IActionResult> GetBlockList(int systemID)
        {
            List<MasterBlock> blockList = await _dbContext.MasterBlocks.Include(u=> u.System).Where(u => u.SystemId == systemID).ToListAsync();


            return Ok(blockList);
        }

        [HttpGet]
        [Route("GetFloorList")]
        public async Task<IActionResult> GetFloorList(int blockID)
        {
            List<MasterFloor> floorList = await _dbContext.MasterFloors.Where(U => U.BlockId == blockID).ToListAsync();

            return Ok(floorList);
        }

        [HttpPost]
        [Route("AddBlock")]
        public async Task<IActionResult> AddBlock(string blockName, int CreatedById, int systemID)
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
                var newBlock = new MasterBlock
                {
                    BlockName = blockName,
                    CreatedById = CreatedById,
                    CreatedDate = providedCreatedDate,
                    SystemId = systemID
                };

                dbContext.MasterBlocks.Add(newBlock);
                dbContext.SaveChanges();
            }


            List<MasterBlock> blockList = await _dbContext.MasterBlocks.Where(u => u.BlockName == blockName).ToListAsync();


            return Ok(blockList);
        }

        [HttpPost]
        [Route("AddFloor")]
        public async Task<IActionResult> AddFloor(string floorName, int blockID, int CreatedById)
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
                var newFloor = new MasterFloor
                {
                    FloorName = floorName,
                    BlockId = blockID,
                    CreatedDate = providedCreatedDate,
                    CreatedById = CreatedById
                };

                dbContext.MasterFloors.Add(newFloor);
                dbContext.SaveChanges();
            }


            List<MasterFloor> floorList = await _dbContext.MasterFloors.Where(u => u.FloorName == floorName).ToListAsync();


            return Ok(floorList);
        }


        [HttpPost]
        [Route("DeleteBlock")]
        public async Task<IActionResult> DeleteBlock(int blockID)
        {
            var blockToDelete = _dbContext.MasterBlocks.Find(blockID); // Assuming blockId is the ID of the block to delete

            // If the block is found, remove it
            if (blockToDelete != null)
            {
                _dbContext.MasterBlocks.Remove(blockToDelete);

                // Save changes to persist the deletion
                _dbContext.SaveChanges();

                var floorsToDelete = _dbContext.MasterFloors.Where(floor => floor.BlockId == blockID);

                if (floorsToDelete != null) { _dbContext.MasterFloors.RemoveRange(floorsToDelete); }

                return Ok(); // or return some other response indicating success
            }
            else
            {
                return NotFound(); // or return some other response indicating that the block was not found
            }

        }



    }
}

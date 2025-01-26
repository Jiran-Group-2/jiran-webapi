using Jiran.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
            string connectionString = "Host=dpg-cub3rrrqf0us73ccgbd0-a.singapore-postgres.render.com;Port=5432;Database=jiran;Username=jiran;Password=OIdjVxKzGqK58hPT8nUiXjQlS2i9UplX;SSL Mode=Require;Trust Server Certificate=true;";

            List<MasterUser> userList = new List<MasterUser>();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync(); // Open the connection asynchronously

                // Define a query to select the user based on the provided username and password
                string query = @"
                SELECT * 
                FROM MasterUser 
                WHERE user_login = @UserLogin AND password = @Password AND status = 'A'";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("UserLogin", username);
                    cmd.Parameters.AddWithValue("Password", password);

                    using (NpgsqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            MasterUser masterUser = new MasterUser
                            {
                                UserId = int.Parse(dr["user_id"]?.ToString()),
                                UserLogin = dr["user_login"]?.ToString(),
                                Password = dr["password"]?.ToString(),
                                Name = dr["name"]?.ToString(),
                                Nric = dr["nric"]?.ToString(),
                                Status = dr["status"]?.ToString(),
                                MobileNo = dr["mobile_no"]?.ToString()
                            };

                            userList.Add(masterUser); // Add the user to the list
                        }
                    }
                }
            }

            if (userList != null && userList.Count > 0)
                return Ok(userList);  // Return the user list if found
            else
                return BadRequest("No user found");  // Return error if no user is found
        }

        [HttpGet]
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            string connectionString = "Host=dpg-cub3rrrqf0us73ccgbd0-a.singapore-postgres.render.com;Port=5432;Database=jiran;Username=jiran;Password=OIdjVxKzGqK58hPT8nUiXjQlS2i9UplX;SSL Mode=Require;Trust Server Certificate=true;";

            List<MasterUser> userList = new List<MasterUser>();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync(); // Open the connection asynchronously

                // Define a query
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM MasterUser", conn);

                using (NpgsqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    var columnNames = Enumerable.Range(0, dr.FieldCount)
                                            .Select(dr.GetName)
                                            .ToList();

                    while (await dr.ReadAsync()) // Loop through each row in the result
                    {
                        MasterUser masterUser = new MasterUser
                        {
                            UserId = int.Parse(dr["user_id"]?.ToString()),
                            UserLogin = dr["user_login"]?.ToString(),
                            Password = dr["password"]?.ToString(),
                            Name = dr["name"]?.ToString(),
                            Nric = dr["nric"]?.ToString(),
                            Status = dr["status"]?.ToString(),
                            MobileNo = dr["mobile_no"]?.ToString()
                        };

                        userList.Add(masterUser); // Add each user to the list
                    }
                }
            }

            return Ok(userList);
            //List<MasterUser> userList = await _dbContext.MasterUsers.ToListAsync();

            //if (userList != null && userList.Count > 0) return Ok(userList);
            //else return BadRequest("No user Found");
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
            string connectionString = "Host=dpg-cub3rrrqf0us73ccgbd0-a.singapore-postgres.render.com;Port=5432;Database=jiran;Username=jiran;Password=OIdjVxKzGqK58hPT8nUiXjQlS2i9UplX;SSL Mode=Require;Trust Server Certificate=true;";

            // Create the MasterUser object with the provided values
            MasterUser newUser = new MasterUser
            {
                UserLogin = providedUserLogin,
                Password = providedPassword,  // Consider hashing the password before storing
                Name = providedName,
                TitleId = providedTitle,
                Nric = providedNric,
                Status = "A",  // Assuming the default status is 'A' for active
                //UnitNumber = providedUnitNo,
                Email = providedHomeNo,  // Assuming email is provided as home number (adjust as necessary)
                MobileNo = providedMobileNo
            };

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync(); // Open the connection asynchronously

                // Define the INSERT query to add the new user to the MasterUser table
                string insertQuery = @"
            INSERT INTO MasterUser 
            (UserLogin, Password, Name, TitleId, Nric, Status, UnitNumber, Email, MobileNo)
            VALUES 
            (@UserLogin, @Password, @Name, @TitleId, @Nric, @Status, @UnitNumber, @Email, @MobileNo)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("UserLogin", newUser.UserLogin);
                    cmd.Parameters.AddWithValue("Password", newUser.Password);
                    cmd.Parameters.AddWithValue("Name", newUser.Name);
                    cmd.Parameters.AddWithValue("TitleId", newUser.TitleId);
                    cmd.Parameters.AddWithValue("Nric", newUser.Nric);
                    cmd.Parameters.AddWithValue("Status", newUser.Status);
                    cmd.Parameters.AddWithValue("UnitNumber", newUser.UnitNumber);
                    cmd.Parameters.AddWithValue("Email", newUser.Email); // Adjusted to match the input type
                    cmd.Parameters.AddWithValue("MobileNo", newUser.MobileNo);

                    // Execute the INSERT command
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return Ok("User registered successfully.");
                    }
                    else
                    {
                        return BadRequest("Failed to register user.");
                    }
                }
            }
            ////DateTime providedCreatedDate = DateTime.Now;
            //// Get the Singapore Standard Time zone (used by Malaysia)
            //TimeZoneInfo malaysiaZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

            //// Get the current time in UTC
            //DateTime utcTime = DateTime.UtcNow;

            //// Convert the current UTC time to Malaysia Time
            //DateTime providedCreatedDate = TimeZoneInfo.ConvertTimeFromUtc(utcTime, malaysiaZone);

            //var userToUpdate = _dbContext.MasterUsers.FirstOrDefault(u => u.UserLogin == providedUserLogin );

            //if (userToUpdate != null) { return BadRequest("There has already exist user with the same login!");  }

            //using (var dbContext = new JiranAppContext())
            //{
            //    var newUser = new MasterUser
            //    {
            //        UserLogin = providedUserLogin,
            //        Password = providedPassword,
            //        Name = providedName,
            //        TitleId = providedTitle,
            //        Nric = providedNric,
            //        UnitNumberId = providedUnitNumberId,
            //        MobileNo = providedMobileNo,
            //        HomeNo = providedHomeNo,
            //        Status = "P",
            //        CreatedById = 1,
            //        CreatedDate = providedCreatedDate,
            //        RoleId = providedRoleId
            //    };

            //    dbContext.MasterUsers.Add(newUser);
            //    dbContext.SaveChanges();
            //}


            //List<MasterUser> userList = await _dbContext.MasterUsers.Include(u => u.Role).Where(u => u.UserLogin == providedUserLogin && u.Password == providedPassword).ToListAsync();

            //using (var dbContext = new JiranAppContext())
            //{
            //    var newUnit = new MasterUnit
            //    {
            //        UserId = userList[0].UserId,
            //        UnitNumber = providedUnitNo,
            //        FloorId = providedFloorID,
            //        BlockId = providedBlockID,
            //        CreatedById = userList[0].UserId,
            //        CreatedDate = providedCreatedDate
            //    };

            //    dbContext.MasterUnits.Add(newUnit);
            //    dbContext.SaveChanges();
            //}


            //return Ok(userList);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(int providedUserID, string providedUserLogin, string providedPassword, string providedName, int providedTitle, string providedNric,
            int providedUnitNumberId, string providedMobileNo, string providedHomeNo, string providedStatus)
        {
            var userToUpdate = _dbContext.MasterUsers.FirstOrDefault(u => u.UserId == providedUserID );

            // If the user is found, update its properties
            if (userToUpdate != null)
            {
                userToUpdate.Name = providedName == null ? userToUpdate.Name: providedName;
                userToUpdate.TitleId = providedTitle == 0 ? userToUpdate.TitleId: providedTitle;
                userToUpdate.Nric = providedNric == null ? userToUpdate.Nric: providedNric;
                //userToUpdate.UnitNumberId = providedUnitNumberId;
                userToUpdate.MobileNo = providedMobileNo == null ? userToUpdate.MobileNo: providedMobileNo;
                userToUpdate.HomeNo = providedHomeNo == null ? userToUpdate.HomeNo: providedHomeNo;
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
            // Get the Singapore Standard Time zone (used by Malaysia)
            TimeZoneInfo malaysiaZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

            // Get the current time in UTC
            DateTime utcTime = DateTime.UtcNow;

            // Convert the current UTC time to Malaysia Time
            DateTime providedCreatedDate = TimeZoneInfo.ConvertTimeFromUtc(utcTime, malaysiaZone);

            string connectionString = "Host=dpg-cub3rrrqf0us73ccgbd0-a.singapore-postgres.render.com;Port=5432;Database=jiran;Username=jiran;Password=OIdjVxKzGqK58hPT8nUiXjQlS2i9UplX;SSL Mode=Require;Trust Server Certificate=true;";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync(); // Open the connection asynchronously

                // Insert visitor data
                string insertQuery = @"INSERT INTO MasterVisitor (visitor_name, visitor_mobile_no, visitor_nric, visitor_quantity, visitor_purpose_of_visit, visitor_vehicle_plate, approval_status, unit_number_id, created_by_id, created_date)
                               VALUES (@VisitorName, @VisitorMobileNo, @VisitorNRIC, @VisitorQuantity, @VisitorPurposeOfVisit, @VisitorVehiclePlate, 'P', @UnitNumberId, @CreatedById, @CreatedDate)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("VisitorName", providedVisitorName);
                    cmd.Parameters.AddWithValue("VisitorMobileNo", providedVisitorMobileNo);
                    cmd.Parameters.AddWithValue("VisitorNRIC", providedVisitorNRIC);
                    cmd.Parameters.AddWithValue("VisitorQuantity", providedQuantity);
                    cmd.Parameters.AddWithValue("VisitorPurposeOfVisit", providedPurposeOfVisit);
                    cmd.Parameters.AddWithValue("VisitorVehiclePlate", providedPlateNo);
                    cmd.Parameters.AddWithValue("UnitNumberId", 1);
                    cmd.Parameters.AddWithValue("CreatedById", 1);
                    cmd.Parameters.AddWithValue("CreatedDate", providedCreatedDate);

                    // Execute the query to insert the data
                    await cmd.ExecuteNonQueryAsync();
                }

                // After inserting, retrieve the inserted visitor data
                string selectQuery = "SELECT * FROM MasterVisitor WHERE visitor_name = @VisitorName AND visitor_mobile_no = @VisitorMobileNo";

                List<MasterVisitor> visitorList = new List<MasterVisitor>();

                using (NpgsqlCommand cmd = new NpgsqlCommand(selectQuery, conn))
                {
                    // Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("VisitorName", providedVisitorName);
                    cmd.Parameters.AddWithValue("VisitorMobileNo", providedVisitorMobileNo);

                    using (NpgsqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            MasterVisitor masterVisitor = new MasterVisitor
                            {
                                VisitorId = Convert.ToInt32(dr["visitor_id"]),
                                VisitorName = dr["visitor_name"]?.ToString(),
                                VisitorMobileNo = dr["visitor_mobile_no"]?.ToString(),
                                VisitorNRIC = dr["visitor_nric"]?.ToString(),
                                VisitorQuantity = Convert.ToInt32(dr["visitor_quantity"]),
                                VisitorPurposeOfVisit = dr["visitor_purpose_of_visit"]?.ToString(),
                                //VisitorVehicleType = Convert.ToInt32(dr["visitor_vehicle_type"]),
                                VisitorVehiclePlate = dr["visitor_vehicle_plate"]?.ToString(),
                                ApprovalStatus = dr["approval_status"]?.ToString(),
                                UnitNumberId = Convert.ToInt32(dr["unit_number_id"])
                            };


                            visitorList.Add(masterVisitor);
                        }
                    }
                }

                if (visitorList.Count > 0)
                {
                    return Ok(visitorList); // Return the inserted visitor data
                }
                else
                {
                    return BadRequest("Failed to insert visitor"); // Return error if no visitor data is found
                }
            }
        }


        [HttpPost]
        [Route("UpdateVisitor")]
        public async Task<IActionResult> UpdateVisitor(int providedVisitorID, string providedVisitorName, string providedVisitorMobileNo, string providedVisitorNRIC, int providedQuantity, string providedPurposeOfVisit, int providedVehicleType,
string providedPlateNo, string providedStatus)
        {
            string connectionString = "Host=dpg-cub3rrrqf0us73ccgbd0-a.singapore-postgres.render.com;Port=5432;Database=jiran;Username=jiran;Password=OIdjVxKzGqK58hPT8nUiXjQlS2i9UplX;SSL Mode=Require;Trust Server Certificate=true;";

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync(); // Open the connection asynchronously

                // Fetch the existing visitor to check if the provided ID exists
                string selectQuery = "SELECT * FROM MasterVisitor WHERE visitor_id = @VisitorId";

                using (NpgsqlCommand selectCmd = new NpgsqlCommand(selectQuery, conn))
                {
                    selectCmd.Parameters.AddWithValue("VisitorId", providedVisitorID);
                    using (NpgsqlDataReader dr = await selectCmd.ExecuteReaderAsync())
                    {
                        if (!dr.Read())
                        {
                            return BadRequest("Visitor not found");
                        }
                    }
                }

                // Update visitor details
                string updateQuery = @"
                UPDATE MasterVisitor
                SET
                    visitor_name = COALESCE(@VisitorName, visitor_name),
                    visitor_mobile_no = COALESCE(@VisitorMobileNo, visitor_mobile_no),
                    visitor_nric = COALESCE(@VisitorNRIC, visitor_nric),
                    visitor_quantity = COALESCE(@VisitorQuantity, visitor_quantity),
                    visitor_purpose_of_visit = COALESCE(@VisitorPurposeOfVisit, visitor_purpose_of_visit),
                    visitor_vehicle_plate = COALESCE(@VisitorVehiclePlate, visitor_vehicle_plate),
                    approval_status = @ApprovalStatus
                WHERE visitor_id = @VisitorId";

                using (NpgsqlCommand updateCmd = new NpgsqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("VisitorId", providedVisitorID);
                    updateCmd.Parameters.AddWithValue("VisitorName", providedVisitorName ?? (object)DBNull.Value);
                    updateCmd.Parameters.AddWithValue("VisitorMobileNo", providedVisitorMobileNo ?? (object)DBNull.Value);
                    updateCmd.Parameters.AddWithValue("VisitorNRIC", providedVisitorNRIC ?? (object)DBNull.Value);
                    updateCmd.Parameters.AddWithValue("VisitorQuantity", providedQuantity != 0 ? providedQuantity : (object)DBNull.Value);
                    updateCmd.Parameters.AddWithValue("VisitorPurposeOfVisit", providedPurposeOfVisit ?? (object)DBNull.Value);
                    updateCmd.Parameters.AddWithValue("VisitorVehiclePlate", providedPlateNo ?? (object)DBNull.Value);
                    updateCmd.Parameters.AddWithValue("ApprovalStatus", providedStatus ?? (object)DBNull.Value);

                    int rowsAffected = await updateCmd.ExecuteNonQueryAsync();
                    if (rowsAffected == 0)
                    {
                        return BadRequest("Failed to update visitor");
                    }
                }
            }

            return Ok();
        }


        [HttpGet]
        [Route("GetVisitor")]
        public async Task<IActionResult> GetVisitor(int? unitUserID)
        {
            string connectionString = "Host=dpg-cub3rrrqf0us73ccgbd0-a.singapore-postgres.render.com;Port=5432;Database=jiran;Username=jiran;Password=OIdjVxKzGqK58hPT8nUiXjQlS2i9UplX;SSL Mode=Require;Trust Server Certificate=true;";

            List<MasterVisitor> visitorList = new List<MasterVisitor>();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync(); // Open the connection asynchronously

                string query = "SELECT * FROM MasterVisitor";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    if (unitUserID != null)
                    {
                        cmd.Parameters.AddWithValue("UnitUserID", unitUserID);
                    }

                    using (NpgsqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        var columnNames = Enumerable.Range(0, dr.FieldCount)
                                            .Select(dr.GetName)
                                            .ToList();

                        while (await dr.ReadAsync())
                        {
                            MasterVisitor masterVisitor = new MasterVisitor
                            {
                                VisitorId = Convert.ToInt32(dr["visitor_id"]),
                                VisitorName = dr["visitor_name"]?.ToString(),
                                VisitorMobileNo = dr["visitor_mobile_no"]?.ToString(),
                                VisitorNRIC = dr["visitor_nric"]?.ToString(),
                                VisitorQuantity = Convert.ToInt32(dr["visitor_quantity"]),
                                VisitorPurposeOfVisit = dr["visitor_purpose_of_visit"]?.ToString(),
                                //VisitorVehicleType = Convert.ToInt32(dr["visitor_vehicle_type"]),
                                VisitorVehiclePlate = dr["visitor_vehicle_plate"]?.ToString(),
                                ApprovalStatus = dr["approval_status"]?.ToString(),
                                UnitNumberId = Convert.ToInt32(dr["unit_number_id"])
                            };

                            visitorList.Add(masterVisitor); // Add each visitor to the list
                        }
                    }
                }
            }

            if (visitorList.Count > 0)
            {
                return Ok(visitorList);
            }
            else
            {
                return BadRequest("No visitor Found");
            }
        }

    }
}

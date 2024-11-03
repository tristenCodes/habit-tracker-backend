using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace HabitTrackerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitController : ControllerBase
    {
        private readonly SqliteConnection _connection;

        public HabitController(SqliteConnection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public IActionResult GetAllHabits()
        {
            var createTable = _connection.CreateCommand();
            createTable.CommandText =
            @"CREATE TABLE TestTable (ID int)";
            createTable.ExecuteReader();

            var command = _connection.CreateCommand();
            command.CommandText =
            @"SELECT * FROM TestTable";
            command.ExecuteReader();
            var obj = new
            {
                worked = true,
            };
            return Ok(obj);
        }
    }
}

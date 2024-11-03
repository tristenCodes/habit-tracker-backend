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
            Object[] RowValues = new Object[10];
            var createTable = _connection.CreateCommand();
            createTable.CommandText =
            @"CREATE TABLE IF NOT EXISTS TestTable (ID INTEGER PRIMARY KEY AUTOINCREMENT, HABIT_NAME TEXT, DATE TEXT, COMPLETED INTEGER);
            INSERT INTO TestTable VALUES(1, 'mow the lawn', datetime(), 0);
            INSERT INTO TestTable VALUES(2, 'mow the fridge', datetime(), 0);
            ";
            createTable.ExecuteNonQuery();

            var command = _connection.CreateCommand();
            command.CommandText =
            @"SELECT * FROM TestTable";
            var reader = command.ExecuteReader();
            if (reader.HasRows && reader.Read())
            {
                reader.GetValues(RowValues);
                return Ok(new { result = RowValues });
            }
            return Ok(new { worked = false });
        }
    }
}

using HabitTrackerBackend.Handlers;
using HabitTrackerBackend.Helpers;
using HabitTrackerBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HabitTrackerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private readonly DatabaseHelper _dbHelper;

        public HabitsController(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        [HttpGet]
        public IActionResult GetHabits()
        {
            var reader = _dbHelper.QueryAllHabits();
            while (reader.Read())
            {
                var tempArray = new Object[reader.FieldCount];
                reader.GetValues(tempArray);
                HabitHandler.PlaceHabitIntoListOfHabits(tempArray);
            }
            return Ok(HabitHandler.AllHabits);
        }

        [HttpGet("{id}")]
        public IActionResult GetHabit(int id)
        {
            var reader = _dbHelper.QueryHabit(id);
            if (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                var result = new { id = values[0], name = values[1], completed = values[2] };
                return Ok(result);
            }
            return NotFound($"Habit ID {id} not found in database");
        }

        [HttpPost]
        public IActionResult CreateHabit([FromBody] Habit habit)
        {
            _dbHelper.InsertHabit(habit.Name, habit.Completed);

            return Ok("Habit created successfully.");
        }

        [HttpDelete]
        public IActionResult DeleteHabit([FromQuery] int id)
        {
            try
            {
                _dbHelper.DeleteHabit(id);
                return Ok($"Habit with ID {id} deleted successfully.");
            }

            catch
            {
                return NotFound($"Habit with ID {id} not found or could not be deleted.");
            }
        }
    }


}

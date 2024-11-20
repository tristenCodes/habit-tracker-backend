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
            var habits = new List<Habit>();
            var reader = _dbHelper.QueryAllHabits();
            while (reader.Read())
            {
                var tempArray = new Object[reader.FieldCount];
                reader.GetValues(tempArray);
                var habit = HabitHandler.TranslateDataIntoHabit(tempArray);
                habits.Add(habit);
            }

            return Ok(habits);
        }

        [HttpGet("{id}")]
        public IActionResult GetHabit(int id)
        {
            var reader = _dbHelper.QueryHabit(id);
            if (reader.Read())
            {
                var values = new object[reader.FieldCount];
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
            var result = new
            {
                Result = "Success. Habit created.",
                Habit = new { habit.Name, habit.Completed }
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHabit(int id)
        {
            var result = _dbHelper.DeleteHabit(id);
            return Ok(result);
        }
    }
}
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
            catch (Exception ex)
            {
                return NotFound($"Habit with ID {id} not found or could not be deleted.");
            }
        }
    }


}

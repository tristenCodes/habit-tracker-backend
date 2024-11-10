using HabitTrackerBackend.Handlers;
using HabitTrackerBackend.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HabitTrackerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitController : ControllerBase
    {
        private readonly DatabaseHelper _dbHelper;

        public HabitController(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        [HttpGet]
        public IActionResult GetHabits()
        {
            _dbHelper.InitializeTable();
            _dbHelper.InsertHabit("Mow the lawn", 1);
            _dbHelper.InsertHabit("Mow the fridge", 0);

            var reader = _dbHelper.QueryAllHabits();
            while (reader.Read())
            {
                var tempArray = new Object[reader.FieldCount];
                reader.GetValues(tempArray);
                HabitHandler.PlaceHabitIntoListOfHabits(tempArray);
            }
            return Ok(HabitHandler.AllHabits);
        }
    }
}

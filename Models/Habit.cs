namespace HabitTrackerBackend.Models;
public class Habit
{
    public long ID { get; set; }
    public string Name { get; set; } = "";
    public long Completed { get; set; }
}
using HabitTrackerBackend.Models;

namespace HabitTrackerBackend.Handlers;

public static class HabitHandler
{
    public static List<Habit> AllHabits { get; set; } = [];
    
    public static Habit TranslateDataIntoHabit(Object[] arr)
    {
        return new Habit
        {
            ID = (long)arr[0],
            Name = (string)arr[1],
            Completed = (long)arr[2],
        };
    }

    private static void AddHabitToListOfHabits(Habit habit)
    {
        if (HabitExistsInList(habit)) return;
        AllHabits.Add(habit);
    }

    private static bool HabitExistsInList(Habit habit)
    {
        var res = AllHabits.Where(h => h.ID == habit.ID);
        return res.Any();
    }

}
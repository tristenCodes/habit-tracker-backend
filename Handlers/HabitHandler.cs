using HabitTrackerBackend.Models;

namespace HabitTrackerBackend.Handlers;

public static class HabitHandler
{
    public static List<Habit> AllHabits { get; set; } = [];

    public static void PlaceHabitIntoListOfHabits(Object[] arr)
    {
        var habit = TranslateDataIntoHabit(arr);
        AddHabitToListOfHabits(habit);
    }

    private static Habit TranslateDataIntoHabit(Object[] arr)
    {
        Habit habit = new Habit();
        habit.ID = (long)arr[0];
        habit.Name = (string)arr[1];
        habit.Completed = (long)arr[2];
        return habit;
    }

    private static void AddHabitToListOfHabits(Habit habit)
    {
        AllHabits.Add(habit);
    }

}
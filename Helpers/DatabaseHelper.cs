namespace HabitTrackerBackend.Helpers;
using Microsoft.Data.Sqlite;

public class DatabaseHelper
{
    private SqliteConnection _connection;
    public DatabaseHelper(SqliteConnection connection)
    {
        _connection = connection;
    }

    public void InitializeTable()
    {
        var command = _connection.CreateCommand();
        command.CommandText =
        @"CREATE TABLE IF NOT EXISTS Habits 
            (ID INTEGER PRIMARY KEY AUTOINCREMENT, HABIT_NAME TEXT, COMPLETED INTEGER)";

        command.ExecuteNonQuery();
    }

    public SqliteDataReader QueryAllHabits()
    {
        var command = _connection.CreateCommand();
        command.CommandText = $"SELECT * FROM Habits";
        var reader = command.ExecuteReader();
        return reader;
    }

    public SqliteDataReader QueryHabit(long id)
    {
        var command = _connection.CreateCommand();
        command.CommandText = $"SELECT * FROM Habits WHERE {id}";
        var reader = command.ExecuteReader();
        return reader;
    }

    public void InsertHabit(string Name, long Completed)
    {
        var command = _connection.CreateCommand();
        command.CommandText =
        $"INSERT INTO Habits (HABIT_NAME, COMPLETED) VALUES ('{Name}', {Completed})";
        command.ExecuteNonQuery();
    }
}
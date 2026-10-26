
using System;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        // Use your existing MySQL connection settings.
        string connectionString =
            "Server=localhost;Database=StudentSystem;" +
            "User ID=stdnt_test;Password=stdntTest;";

        string sql = @"
            SELECT StudentID, FirstName, LastName,
                   DateOfBirth, YearEntry
            FROM Student";

        using MySqlConnection connection =
            new MySqlConnection(connectionString);

        connection.Open();

        using MySqlCommand command =
            new MySqlCommand(sql, connection);

        using MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Student student = new Student
            {
                StudentID = reader.GetInt32("StudentID"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),


                DateOfBirth = reader.IsDBNull(
                reader.GetOrdinal("DateOfBirth"))
                ? (DateTime?)null
                 : reader.GetDateTime(
                 reader.GetOrdinal("DateOfBirth")),

                YearEntry = reader.IsDBNull(
                reader.GetOrdinal("YearEntry"))
                  ? (int?)null
                  : reader.GetInt32(
                  reader.GetOrdinal("YearEntry"))
                 };

            Console.WriteLine(
                $"ID: {student.StudentID}, " +
                $"Name: {student.FirstName} {student.LastName}, " +
                $"DOB: {student.DateOfBirth?.ToString("yyyy-MM-dd") ?? "Unknown"}, " +
                $"Year Entered: {student.YearEntry?.ToString() ?? "Unknown"}");
        }
    }
}

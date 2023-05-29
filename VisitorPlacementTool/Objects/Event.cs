using Microsoft.Extensions.Logging;
using Npgsql;
using VisitorPlacementTool.postgres;

namespace VisitorPlacementTool;

public class Event
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int MaxVisitors { get; private set; }
    public DateTime DateTime { get; private set; }

    public Event WithId(int id)
    {
        Id = id;
        return this;
    }

    public Event WithName(string name)
    {
        Name = name;
        return this;
    }

    public Event WithMaxVisitors(int maxVisitors)
    {
        MaxVisitors = maxVisitors;
        return this;
    }

    public Event WithDateTime(DateTime dateTime)
    {
        DateTime = dateTime;
        return this;
    }

    public int PostToDb()
    {
        using (var conn = new NpgsqlConnection(new DatabaseHandling().GetDatabaseConnectionString()))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO events (name, max_visitors, creation_datetime) VALUES (@name, @max_visitors, @datetime)";
                cmd.Parameters.AddWithValue("memberids", Name);
                cmd.Parameters.AddWithValue("eventid", MaxVisitors);
                cmd.Parameters.AddWithValue("datetime", DateTime);
                var response = cmd.ExecuteNonQuery();
                return response;
            }
        }
    }
}
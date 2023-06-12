using Microsoft.Extensions.Logging;
using Npgsql;
using VisitorPlacementTool.postgres;

namespace VisitorPlacementTool;

public class Event
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int MaxVisitors { get; private set; }
    public DateTime CreationDate { get; set; }
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

    public Event WithCreationDate(DateTime dateTime)
    {
        CreationDate = dateTime;
        return this;
    }

    public void GetFromDb(int eventId)
    {
        using (var conn = new NpgsqlConnection(new DatabaseHandling().GetDatabaseConnectionString()))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM events WHERE id = " + eventId;
                var dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Name = dataReader.GetString(dataReader.GetOrdinal("name"));
                    MaxVisitors = dataReader.GetInt32(dataReader.GetOrdinal("max_visitors"));
                    CreationDate = dataReader.GetDateTime(dataReader.GetOrdinal("creation_datetime"));
                    DateTime = dataReader.GetDateTime(dataReader.GetOrdinal("datetime"));
                }
            }
        }
    }
    
    public int PostToDb()
    {
        using (var conn = new NpgsqlConnection(new DatabaseHandling().GetDatabaseConnectionString()))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO events (name, max_visitors, creation_datetime, datetime) VALUES (@name, @max_visitors, @creation_datetime, @datetime)";
                cmd.Parameters.AddWithValue("name", Name);
                cmd.Parameters.AddWithValue("max_visitors", MaxVisitors);
                cmd.Parameters.AddWithValue("creation_datetime", CreationDate);
                cmd.Parameters.AddWithValue("datetime", DateTime);
                var response = cmd.ExecuteNonQuery();
                return response;
            }
        }
    }
}
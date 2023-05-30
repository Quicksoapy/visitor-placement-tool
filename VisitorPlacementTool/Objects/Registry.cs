using Microsoft.Extensions.Logging;
using Npgsql;
using VisitorPlacementTool.Interfaces;
using VisitorPlacementTool.postgres;

namespace VisitorPlacementTool;

public class Registry
{
    public int Id { get; private set; }
    public string Visitor { get; private set; }
    public int EventId { get; private set; }
    public DateTime DateTime { get; private set; }

    public Registry WithId(int id)
    {
        Id = id;
        return this;
    }

    public Registry WithVisitor(string visitor)
    {
        Visitor = visitor;
        return this;
    }
    
    public Registry WithEventId(int eventId)
    {
        EventId = eventId;
        return this;
    }

    public Registry WithDateTime(DateTime dateTime)
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
                cmd.CommandText = "INSERT INTO individual_registrations (visitor, event_id, registry_datetime) VALUES (@visitor, @eventid, @datetime)";
                cmd.Parameters.AddWithValue("visitor", Visitor);
                cmd.Parameters.AddWithValue("eventid", EventId);
                cmd.Parameters.AddWithValue("datetime", DateTime);
                var response = cmd.ExecuteNonQuery();
                return response;
            }
        }
    }
}
using Npgsql;
using VisitorPlacementTool.Interfaces;
using VisitorPlacementTool.postgres;

namespace VisitorPlacementTool;

public class Group
{
    public int Id { get; private set; }
    public string[] Members { get; private set; }
    public DateTime[] Birthdays { get; private set; }
    public int EventId { get; private set; }
    public DateTime DateTime { get; private set; }

    public Group WithId(int id)
    {
        Id = id;
        return this;
    }

    public Group WithMembers(string[] members)
    {
        Members = members;
        return this;
    }
    
    public Group WithBirthdays(DateTime[] birthdays)
    {
        Birthdays = birthdays;
        return this;
    }

    public Group WithEventId(int eventId)
    {
        EventId = eventId;
        return this;
    }

    public Group WithDateTime(DateTime dateTime)
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
                cmd.CommandText = "INSERT INTO groups (members, event_id, registry_datetime) VALUES (@memberids, @eventid, @datetime)";
                cmd.Parameters.AddWithValue("memberids", Members);
                cmd.Parameters.AddWithValue("eventid", EventId);
                cmd.Parameters.AddWithValue("datetime", DateTime);
                var response = cmd.ExecuteNonQuery();
                return response;
            }
        }
    }
}
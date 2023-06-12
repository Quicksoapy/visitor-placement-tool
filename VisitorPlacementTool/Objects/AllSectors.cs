using Microsoft.Extensions.Logging;
using Npgsql;
using VisitorPlacementTool.postgres;

namespace VisitorPlacementTool;

public class AllSectors
{
    public List<Sector> Sectors { get; private set; }
    public int MaxVisitors { get; private set; }

    public AllSectors WithSectors(List<Sector> sectors)
    {
        Sectors = sectors;
        return this;
    }

    public AllSectors WithMaxVisitors(int maxVisitors)
    {
        MaxVisitors = maxVisitors;
        return this;
    }

    public Registrations GetRegistrations(int eventId)
    {
        var registries = new List<Registry>();
        var groups = new List<Group>();
        
        using (var conn = new NpgsqlConnection(new DatabaseHandling().GetDatabaseConnectionString()))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT individual_registrations.id, individual_registrations.visitor, individual_registrations.event_id, individual_registrations.registry_datetime, visitors.birthday FROM individual_registrations JOIN visitors ON individual_registrations.visitor = visitors.name WHERE event_id = " + eventId;
                var dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    var registry = new Registry()
                        .WithId(dataReader.GetInt32(dataReader.GetOrdinal("id")))
                        .WithVisitor(dataReader.GetString(dataReader.GetOrdinal("visitor")))
                        .WithEventId(dataReader.GetInt32(dataReader.GetOrdinal("event_id")))
                        .WithDateTime(dataReader.GetDateTime(dataReader.GetOrdinal("registry_datetime")))
                        .WithBirthday(dataReader.GetDateTime(dataReader.GetOrdinal("birthday")));
                    
                    registries.Add(registry);
                }
            }
        }

        using (var conn = new NpgsqlConnection(new DatabaseHandling().GetDatabaseConnectionString()))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM groups WHERE event_id = " + eventId;
                var dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    var group = new Group()
                        .WithId(dataReader.GetInt32(dataReader.GetOrdinal("id")))
                        .WithMembers(dataReader.GetFieldValue<string[]>(3))
                        .WithEventId(dataReader.GetInt32(dataReader.GetOrdinal("event_id")))
                        .WithDateTime(dataReader.GetDateTime(dataReader.GetOrdinal("registry_datetime")));

                    groups.Add(group);
                }
            }
        }
        
        return new Registrations().WithGroups(groups).WithRegistries(registries);
    }
}
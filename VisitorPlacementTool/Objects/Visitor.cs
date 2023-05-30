using Microsoft.Extensions.Logging;
using Npgsql;
using VisitorPlacementTool.postgres;

namespace VisitorPlacementTool;

public class Visitor
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public DateTime Birthday { get; private set; }

    public Visitor WithId(int id)
    {
        Id = id;
        return this;
    }

    public Visitor WithName(string name)
    {
        Name = name;
        return this;
    }

    public Visitor WithBirthday(DateTime birthday)
    {
        Birthday = birthday;
        return this;
    }

    public bool IsAdult(DateTime date)
    {
        return date.Year - Birthday.Year >= 13;
    }
    
    public int PostToDb()
    {
        using (var conn = new NpgsqlConnection(new DatabaseHandling().GetDatabaseConnectionString()))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO visitors (name, birthday) VALUES (@name, @birthday)";
                cmd.Parameters.AddWithValue("name", Name);
                cmd.Parameters.AddWithValue("birthday", Birthday);
                var response = cmd.ExecuteNonQuery();
                return response;
            }
        }
    }
}
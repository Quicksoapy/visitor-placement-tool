namespace VisitorPlacementTool;

public class Group
{
    public int Id { get; private set; }
    public List<Visitor> Members { get; private set; }
    public DateTime DateTime { get; private set; }
    
    public Group WithId(int id)
    {
        Id = id;
        return this;
    }

    public Group WithMembers(List<Visitor> members)
    {
        Members = members;
        return this;
    }

    public Group WithDateTime(DateTime dateTime)
    {
        DateTime = dateTime;
        return this;
    }
}
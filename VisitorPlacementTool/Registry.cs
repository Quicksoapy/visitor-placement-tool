namespace VisitorPlacementTool;

public class Registry
{
    public int Id { get; private set; }
    public Visitor Visitor { get; private set; }
    public DateTime DateTime { get; private set; }

    public Registry WithId(int id)
    {
        Id = id;
        return this;
    }

    public Registry WithVisitor(Visitor visitor)
    {
        Visitor = visitor;
        return this;
    }

    public Registry WithDateTime(DateTime dateTime)
    {
        DateTime = dateTime;
        return this;
    }
}
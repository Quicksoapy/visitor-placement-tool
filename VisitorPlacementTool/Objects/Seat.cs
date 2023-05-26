using VisitorPlacementTool.Interfaces;

namespace VisitorPlacementTool;

public class Seat
{
    public Visitor _visitor { get; private set; }

    public Seat WithVisitor(Visitor visitor)
    {
        _visitor = visitor;
        return this;
    }
}
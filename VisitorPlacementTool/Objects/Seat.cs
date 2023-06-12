using VisitorPlacementTool.Interfaces;

namespace VisitorPlacementTool;

public class Seat
{
    public Visitor Visitor { get; private set; }

    public Seat WithVisitor(Visitor visitor)
    {
        Visitor = visitor;
        return this;
    }
}
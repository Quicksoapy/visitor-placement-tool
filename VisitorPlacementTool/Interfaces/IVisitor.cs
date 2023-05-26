namespace VisitorPlacementTool.Interfaces;

public interface IVisitor
{
    Visitor WithId(int id);
    Visitor WithName(string name);
    Visitor WithBirthday(DateTime birthday);
    bool IsAdult(DateTime date);
}
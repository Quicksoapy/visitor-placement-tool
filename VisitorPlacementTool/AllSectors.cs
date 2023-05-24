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
}
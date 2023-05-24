namespace VisitorPlacementTool;

public class Sector
{
    public int RowLength { get; private set; }
    public int RowCount { get; private set; }
    public List<List<Seat>> SeatMatrix { get; private set; }

    public Sector WithRowLength(int rowLength)
    {
        RowLength = rowLength;
        return this;
    }

    public Sector WithRowCount(int rowCount)
    {
        RowCount = rowCount;
        return this;
    }

    public void AddSeat(List<List<Seat>> seats)
    {
        SeatMatrix = seats;
    }
}
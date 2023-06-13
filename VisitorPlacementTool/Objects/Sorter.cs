namespace VisitorPlacementTool;

public class Sorter
{
    public AllSectors Sort(int eventId)
    {
        var currentEvent = new Event();
        currentEvent.GetFromDb(eventId);
                
        var allSectors = new AllSectors();
        var registrations = allSectors.GetRegistrations(eventId);
        var kidsInGroups = new List<Registry>();
        var kids = new List<Registry>();

        var sectorA = new Sector();
        var sectorB = new Sector();
        var sectorC = new Sector();
        foreach (var group in registrations.Groups)
        {
            var rowA1 = new List<Seat>();
            var rowA2 = new List<Seat>();
            var rowA3 = new List<Seat>();
            for (int j = 0; j < group.Members.Length; j++)
            {
                if (currentEvent.DateTime.Year - group.Birthdays[j].Year >= 13 && rowA1.Count < 10)
                {
                    rowA1.Add(new Seat()
                        .WithVisitor(new Visitor()
                            .WithName(group.Members[j])
                            .WithBirthday(group.Birthdays[j])));
                    continue;
                }

                if (currentEvent.DateTime.Year - group.Birthdays[j].Year >= 13 && rowA2.Count < 10)
                {
                    rowA2.Add(new Seat()
                        .WithVisitor(new Visitor()
                            .WithName(group.Members[j])
                            .WithBirthday(group.Birthdays[j])));
                    continue;
                }
                        
                if (currentEvent.DateTime.Year - group.Birthdays[j].Year >= 13 && rowA3.Count < 10)
                {
                    rowA3.Add(new Seat()
                        .WithVisitor(new Visitor()
                            .WithName(group.Members[j])
                            .WithBirthday(group.Birthdays[j])));
                }
            }

           
        }

        return allSectors;
    }
}
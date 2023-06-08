namespace VisitorPlacementTool;

public class Registrations
{
    public List<Registry> Registries { get; private set; }
    public List<Group> Groups { get; private set; }

    public Registrations WithRegistries(List<Registry> registries)
    {
        Registries = registries;
        return this;
    }
    
    public Registrations WithGroups(List<Group> groups)
    {
        Groups = groups;
        return this;
    }
}
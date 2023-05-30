using System.Globalization;

namespace VisitorPlacementTool;

class Program
{
    static void Main(string[] args)
    {
        
        string name = null;
        string birthday = null;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--signup")
            {
                name = args[i + 1];
                birthday = args[i + 2];

                DateTime date = DateTime.ParseExact(birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var visitor = new Visitor()
                    .WithName(name)
                    .WithBirthday(date);
                visitor.PostToDb();
            }

            string[] names = new string[args.Length - i - 1];
            string eventId = null;
            if (args[i] == "--register")
            {
                eventId = args[i + 1];

                Array.Copy(args, i + 2, names, 0, args.Length - i - 2);
                if (names.Length >= 2)
                {
                    var group = new Group()
                        .WithMembers(names)
                        .WithDateTime(DateTime.Now)
                        .WithEventId(Convert.ToInt32(eventId));
                    group.PostToDb();
                }
                else
                {
                    var registry = new Registry()
                        .WithVisitor(names[0])
                        .WithDateTime(DateTime.Now)
                        .WithEventId(Convert.ToInt32(eventId));
                    registry.PostToDb();
                }
            }

            string eventName = null;
            string maxVisitors = null;
            if (args[i] == "--event")
            {
                eventName = args[i + 1];
                maxVisitors = args[i + 2];

                var @event = new Event()
                    .WithName(eventName)
                    .WithMaxVisitors(Convert.ToInt32(maxVisitors))
                    .WithDateTime(DateTime.Now);
                @event.PostToDb();
            }

            if (args[i] == "--sort")
            {
                eventId = args[i + 1];
                
            }
        }
    }
}
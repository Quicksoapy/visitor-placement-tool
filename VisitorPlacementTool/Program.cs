﻿using System.Globalization;

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

                List<string> namesList = new List<string>(args.Length - 2);

                Array.Copy(args, i + 2, namesList.ToArray(), 0, args.Length - i - 2);

                for (int j = namesList.Count - 1; j >= 0; j--)
                {
                    if (string.IsNullOrWhiteSpace(namesList[j]))
                    {
                        namesList.RemoveAt(j);
                    }
                }

                string[] namesArray = namesList.ToArray();
                
                if (namesArray.Length >= 2)
                {
                    var group = new Group()
                        .WithMembers(namesArray)
                        .WithDateTime(DateTime.Now)
                        .WithEventId(Convert.ToInt32(eventId));
                    group.PostToDb();
                }
                else
                {
                    var registry = new Registry()
                        .WithVisitor(namesArray[0])
                        .WithDateTime(DateTime.Now)
                        .WithEventId(Convert.ToInt32(eventId));
                    registry.PostToDb();
                }
            }

            string eventName = null;
            string maxVisitors = null;
            string datetime = null;
            if (args[i] == "--event")
            {
                eventName = args[i + 1];
                maxVisitors = args[i + 2];
                datetime = args[i + 3];
                
                DateTime date = DateTime.ParseExact(datetime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var @event = new Event()
                    .WithName(eventName)
                    .WithMaxVisitors(Convert.ToInt32(maxVisitors))
                    .WithCreationDate(DateTime.Now)
                    .WithDateTime(date);
                @event.PostToDb();
            }

            if (args[i] == "--sort")
            {
                eventId = args[i + 1];
                var sorter = new Sorter();
                sorter.Sort(Convert.ToInt32(eventId));
            }
        }
    }
}
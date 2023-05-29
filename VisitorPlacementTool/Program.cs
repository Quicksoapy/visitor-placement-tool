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
            }
            
            string[] names = new string[args.Length - i - 1];
            string eventId = null;
            if (args[i] == "--register")
            {
                eventId = args[i + 1];

                Array.Copy(args, i + 2, names, 0, args.Length - i - 2);
                if (names.Length >= 2)
                {

                }
                else
                {
                    
                }
            }

            string eventName = null;
            string maxVisitors = null;
            if (args[i] == "--event")
            {
                eventName = args[i + 1];
                maxVisitors = args[i + 2];
            }
        }
    }
}
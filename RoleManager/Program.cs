using System.Reflection.Metadata.Ecma335;
bool gettingName = true;
bool gettingRole = false;
bool confirmingRole = false;
bool addingAnother = false;
bool exitProgram = false;

string? employeeName = "";
string? employeeRole = "";
string? confirmation = "";
int employeeNumber = 1;
int guestCount = 0;

List<string> names = new List<string>();
List<string> roles = new List<string>();

Console.WriteLine("Hello! Welcome to your company's Employee Role Manager!");

while (!exitProgram)
{
    if (gettingName)
    {
        Console.Write($"Please enter Employee #{employeeNumber}'s name.\n> ");
        employeeName = CheckForNull(Console.ReadLine(), "Please enter the name of your employee:");
        employeeName = char.ToUpper(employeeName[0]) + employeeName.Substring(1).ToLower();

        gettingName = false;
        gettingRole = true;
    }
    if (gettingRole)
    {
        Console.Write($"What role will {employeeName} have on your team?\n\n{RolesAndPermissions()}\n\n> ");
        employeeRole = CheckForNull(Console.ReadLine().ToLower(), $"What role will {employeeName} have on your team?\n\n{RolesAndPermissions()}");

        switch (employeeRole)
        {
            case "administrator":
            case "user":
            case "guest":
                gettingRole = false;
                confirmingRole = true;
                break;
            default:
                Console.Write($"You entered {employeeRole}, which is not a valid role. ");
                break;
        }
    }

    if (confirmingRole)
    {
        Console.Write($"Are you sure you want to grant employee {employeeName} the role of {employeeRole}? [Y/N]\n> ");
        confirmation = CheckForNull(Console.ReadLine().ToLower(), $"Please type \"Y\" or \"N\" to accept or refuse.");

        while (confirmation != "y" && confirmation != "n")
        {
            Console.Write($"You entered {confirmation}, type \"Y\" or \"N\" to accept or refuse.\n> ");
            confirmation = Console.ReadLine().ToLower();
        }

        if (confirmation == "y")
        {
            if (employeeRole == "guest")
            {
                if (guestCount >= 3)
                {
                    Console.WriteLine("You cannot add more guests, there is a MAX of 3.");
                    confirmingRole = false;
                    gettingRole = true;
                    continue;
                }
                guestCount++;
            }
            confirmingRole = false;
            addingAnother = true;
            names.Add(employeeName);
            roles.Add(employeeRole);
        }
        else
        {
            confirmingRole = false;
            gettingRole = true;
            Console.WriteLine();
        }
    }

    if (addingAnother)
    {
        Console.Write("Would you like to add another person to the team? [Y/N]\n> ");
        confirmation = CheckForNull(Console.ReadLine().ToLower(), $"Please type \"Y\" or \"N\" to accept or refuse.");

        while (confirmation != "y" && confirmation != "n")
        {
            Console.Write($"You entered {confirmation}, type \"Y\" or \"N\" to accept or refuse.\n> ");
            confirmation = Console.ReadLine().ToLower();
        }

        if (confirmation == "y")
        {
            employeeNumber++;
            addingAnother = false;
            gettingName = true;
        }
        else
        {
            Console.WriteLine("Thank you for using your company's Employee Role Manager!");
            addingAnother = false;
            exitProgram = true;
        }
    }

    
}

Console.WriteLine("\nYour Team Roster:\nName\t \tRole\n" +
                  "---------------------------------------------");

string[] roleOrder = { "administrator", "user", "guest" };

foreach (string role in roleOrder)
{
    for (int i = 0; i < names.Count; i++)
    {
        if (roles[i] == role)
        {
            Console.WriteLine($"{names[i]}\t-\t{char.ToUpper(roles[i][0]) + roles[i].Substring(1).ToLower()}");
        }
    }
}


string CheckForNull(string input, string repromptUser)
{
    string? correctLine = input;

    while (string.IsNullOrWhiteSpace(correctLine))
    {
        Console.Write($"This entry can't be empty. {repromptUser}\n> ");
        correctLine = Console.ReadLine();
    }
    return correctLine;
}

string RolesAndPermissions()
{
    return $"+ Administrator\n" +
                "\t- Create/Delete users\n" +
                "\t- Modify all settings\n" +
                "\t- CANNOT delete own account (requires 2 admins)\n" +
                "\t- All actions will be recorded (Found in audit log)\n" +
            $"+ User\n" +
                "\t- Standard operations for own account\n" +
                "\t- Can remove guests but cannot operate on members at higher level\n" +
            $"+ Guest\n" +
                "\t- Can ONLY view, no modifications allowed\n" +
               $"\t- There is a limit of 3 guests per team (Current: {guestCount}/3)";
}
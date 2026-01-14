using System.Reflection.Metadata.Ecma335;
bool validInput = false;
string? employeeName = "";
string? employeeRole = "";
string? confirmation = "";

Console.Write("Hello! Welcome to your company's Employee Role Manager!\n\nPlease enter the name of the first employee for this session:\n> ");
employeeName = CheckForNull(Console.ReadLine(), "Please enter a name.");
employeeName = char.ToUpper(employeeName[0]) + employeeName.Substring(1).ToLower();


Console.Write($"What role will {employeeName} have on your team?\n- Administrator\n- User\n- Guest\n> ");
employeeRole = Console.ReadLine().ToLower();


while (!validInput)
{
    switch (employeeRole)
    {
        case "administrator":
        case "user":
        case "guest":
            validInput = true;
            break;
        case "":
            employeeRole = CheckForNull(employeeRole, "Please enter a role.\n- Administrator\n- User\n- Guest");
            continue;
        default:
            Console.Write($"You entered {employeeRole}, the available roles are:\n- Administrator\n- User\n- Guest\n\nPlease enter one of these roles:\n> ");
            employeeRole = Console.ReadLine().ToLower();
            employeeRole = CheckForNull(employeeRole, "Please enter a role.\n- Administrator\n- User\n- Guest");
            continue;
    }
    Console.Write($"Are you sure you want to grant employee {employeeName} the role of {employeeRole}? [Y/N]\n> ");
    confirmation = Console.ReadLine().ToLower();

    //PICK UP FROM HERE - N SHOULD LOOP BACK TO TOP ----------------------------------
    while (confirmation != "y" && confirmation != "n")
    {
        Console.Write($"You entered {confirmation}, type \"Y\" or \"N\" to accept or refuse assignment.\n> ");
        confirmation = Console.ReadLine().ToLower();

        if (confirmation == "y")
        {
            //code here
        }
        else
        {
            //code here
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
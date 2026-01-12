string? numInputs = "";
int convertedInputs = 0;
bool validInput = false;

Console.Write("How many numbers would you like to analyze today?\n> ");

//Makes sure the input is numerical
while (validInput == false)
{
    numInputs = Console.ReadLine();
    validInput = int.TryParse(numInputs, out convertedInputs);

    if (validInput == false)
    {
        Console.Write($"You entered {numInputs}, This is invalid.\n\nPlease enter how many numbers you would like to analyze.\n> ");
    }
}

string? userNumber = "";
int[] convertedNumbers = new int[convertedInputs];

for (int i = 0; i < convertedInputs; i++)
{
    Console.Write($"Please enter number {i + 1}:\t");
    validInput = false;

    //Makes sure the input is numerical
    while (validInput == false)
    {
        userNumber = Console.ReadLine();
        validInput = int.TryParse(userNumber, out convertedNumbers[i]);

        if (validInput == false)
        {
            Console.Write($"You entered {userNumber}, This is invalid.\nPlease enter number {i + 1}\n> ");
        }
    }
}

/*
Console.Write($"The entered numbers are: ");

for (int i = 0; i < convertedNumbers.Length;i++)
{
    Console.Write($"{convertedNumbers[i]}, ");

}
*/

void CalculateAverage(int[] userInput)
{

}
void FindMax(int[] userInput)
{

}

void FindMin(int[] userInput)
{

}

void SumAll(int[] userInput)
{

}

//I'm pretty sure the input validation can be a method, maybe add that later.
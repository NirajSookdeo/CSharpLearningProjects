string? numInputs = "";
int convertedInputs = 0;
bool validInput = false;
string averageCategory = "";

Console.Write("How many numbers would you like to analyze today?\n> ");

//Makes sure the input is numerical
while (!validInput)
{
    numInputs = Console.ReadLine();
    validInput = int.TryParse(numInputs, out convertedInputs);

    if (!validInput)
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
    while (!validInput)
    {
        userNumber = Console.ReadLine();
        validInput = int.TryParse(userNumber, out convertedNumbers[i]);

        if (!validInput)
        {
            Console.Write($"You entered {userNumber}, This is invalid.\nPlease enter number {i + 1}\n> ");
        }
    }
}


Console.Write("\nYou entered: ");

for (int i = 0; i < convertedNumbers.Length;i++)
{
    Console.Write($"{convertedNumbers[i]}");
    if (i != convertedNumbers.Length - 1)
    {
        Console.Write(", ");
    }
    else
    {
        Console.WriteLine();
    }

}

averageCategory = CalculateAverage(convertedNumbers);
FindMax(convertedNumbers);
FindMin(convertedNumbers);
Sum(convertedNumbers);
Console.WriteLine($"Category:\t{averageCategory}");

string CalculateAverage(int[] numbers)
{
    decimal average = 0;
    string averageRating = "";

    for (int i = 0; i < numbers.Length; i++)
    {
        average += numbers[i];
    }

    average /= numbers.Length;

    if (average > 50)
    {
        averageRating = "High average";
    }
    else if (20 <= average && average <= 50)
    {
        averageRating = "Medium average";
    }
    else
    {
        averageRating = "Low average";
    }

    Console.WriteLine($"Average:\t{average:F2}");
    return averageRating;
}
        
void FindMax(int[] numbers)
{
    int max = numbers[0];

    for (int i = 0; i < numbers.Length; i++)
    {
        if (numbers[i] > max)
        {
            max = numbers[i];
        }
    }
    Console.WriteLine($"Maximum:\t{max}");
}

void FindMin(int[] numbers)
{
    int min = numbers[0];

    for (int i = 0; i < numbers.Length; i++)
    {
        if (numbers[i] < min)
        {
            min = numbers[i];
        }
    }
    Console.WriteLine($"Minimum:\t{min}");
}

void Sum(int[] numbers)
{
    int totalSum = 0;

    for (int i = 0; i < numbers.Length; i++)
    {
        totalSum += numbers[i];
    }
    Console.WriteLine($"Total Sum:\t{totalSum}");
}


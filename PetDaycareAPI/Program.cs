using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Pet myPet = new Pet(50, 50, 50);
myPet.Name = "API Pet";

app.MapGet("/pet", () =>
{
    return myPet;
});

app.MapPost("/pet/feed", () =>
{
    myPet.Feed();
    return myPet;
});

app.MapPost("/pet/play", () =>
{
    myPet.Play();
    return myPet;
});

app.MapPost("/pet/sleep", () =>
{
    myPet.Sleep();
    return myPet;
});

app.Run();
public class Pet
{
    public string? Name { get; set; }
    public int Hunger { get; set; } = 0;
    public int Happiness { get; set; } = 0;
    public int Energy { get; set; } = 0;

    public Pet(int hunger, int happiness, int energy)
    {
        Hunger = hunger;
        Happiness = happiness;
        Energy = energy;
    }

    private void ClampStats()
    {
        Hunger = Math.Clamp(Hunger, 0, 100);
        Happiness = Math.Clamp(Happiness, 0, 100);
        Energy = Math.Clamp(Energy, 0, 100);
    }
    public void Play()
    {
        //increase happiness at the expense of energy
        Happiness += 10;
        Energy -= 5;
        Hunger += 5;
        ClampStats();
    }

    public void Feed()
    {
        //decrease hunger at the expense of happiness and energy
        Hunger -= 25;
        Happiness -= 5;
        Energy -= 5;
        ClampStats();
    }

    public void Sleep()
    {
        //increase energy at the expense of happiness and hunger
        Energy += 50;
        Happiness -= 20;
        Hunger += 20;
        ClampStats();
    }

    public string[] GenerateStats()
    {
        int[] stats = { Hunger, Happiness, Energy };
        string[] statsNames = { "Hunger", "Happiness", "Energy" };
        StringBuilder statBar = new StringBuilder(10);
        List<string> statsDisplay = new List<string>();
        int statPosition = 0;

        foreach (var stat in stats)
        {
            int statBlock = stat / 10;

            for (int i = 0; i < statBlock; i++)
            {
                statBar = statBar.Append("█");
            }

            for (int i = statBar.Length; i < 10; i++)
            {
                statBar = statBar.Append("_");
            }

            statsDisplay.Add($"{statsNames[statPosition]}: {stat} {statBar}");
            ++statPosition;
            statBar.Clear();
        }

        return statsDisplay.ToArray();
    }

    public void PetDisplay()
    {
        //displays the pet and changes view based on stats
        if (Happiness >= 70)
        {
            Console.WriteLine(@" 
    /\_/\
   ( ^.^ ) 
    > ^ <  Happy " + Name + "!");
        }
        else if (Happiness >= 30)
        {
            Console.WriteLine(@"
    /\_/\
   ( -.- ) 
    > ^ <  Content " + Name);
        }
        else
        {
            Console.WriteLine(@"
    /\_/\  
   ( >.< ) 
    > ^ <  Sad " + Name + "...");
        }
    }

    public string GetStatusMessage()
    {
        if (Hunger >= 80)
            return "I'm really hungry...";
        else if (Hunger >= 60)
            return "I'm feeling a bit peckish.";
        else if (Hunger <= 20)
            return "I'm full and satisfied!";

        if (Happiness <= 20)
            return "I feel sad... maybe play with me?";
        else if (Happiness <= 40)
            return "I could use some cheering up...";
        else if (Happiness >= 80)
            return "I couldn't be happier!";

        if (Energy <= 20)
            return "I'm so exhausted... Time for rest.";
        else if (Energy <= 40)
            return "I'm a little tired...";
        else if (Energy >= 80)
            return "I'm bursting with energy!";

        return "I'm doing okay!";
    }

    public string PlayReaction()
    {
        if (Happiness >= 90)
            return "Wheee! This is the best day ever!";
        else if (Energy <= 10)
            return "*yawn* I'm too tired to play...";
        else if (Hunger >= 80)
            return "Can we play after a snack? I'm hungry!";
        return "Yay! That was fun!";
    }

    public string FeedReaction()
    {
        if (Hunger <= 10)
            return "*burp* I wasn't even hungry!";
        else if (Happiness <= 20)
            return "Thanks... this cheers me up a little.";
        return "Yummy! Thank you!";
    }

    public string SleepReaction()
    {
        if (Energy >= 90)
            return "*stretches* That was a great nap!";
        else if (Happiness <= 20)
            return "*tosses and turns* I had bad dreams...";
        return "*snore* Zzz...";
    }

    public bool CheckGameOver()
    {
        bool gameContinue = true;

        if (Hunger == 100)
        {
            Console.Clear();
            Console.WriteLine($"Oh no! {Name} starved for far too long...\n\nGAME OVER.");
            gameContinue = false;
        }

        else if (Happiness == 0)
        {
            Console.Clear();
            Console.WriteLine($"Oh no! {Name} ran away from home out of sadness...\n\nGAME OVER.");
            gameContinue = false;
        }

        else if (Energy == 0)
        {
            Console.Clear();
            Console.WriteLine($"Oh no! {Name} has collapsed from exhaustion...\n\nGAME OVER.");
            gameContinue = false;
        }

        return gameContinue;
    }

    public void SaveGame()
    {
        string saveFile = $"{Name}'s_Save.txt";
        string saveData = $"{Name}\n{Hunger}\n{Happiness}\n{Energy}";

        File.WriteAllText(saveFile, saveData);

        Console.WriteLine($"Your pet has been safely stored at {saveFile}!");
    }

    public void LoadGame(string saveFile)
    {
        if (!File.Exists(saveFile))
        {
            Console.WriteLine($"No pets found at {saveFile} unfortunately...\n" +
                              "Try entering it as \"[PET NAME]'s_Save.txt\"");
            return;
        }

        string saveData = File.ReadAllText(saveFile);
        string[] petStats = saveData.Split('\n');

        Name = petStats[0];
        Hunger = int.Parse(petStats[1]);
        Happiness = int.Parse(petStats[2]);
        Energy = int.Parse(petStats[3]);
        ClampStats();

        Console.WriteLine($"Your pet {Name} was brought back from your save at {saveFile}!\n" +
                           "They missed you!");
    }
}

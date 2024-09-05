using Spectre.Console;

public class FlashcardController
{
    public static void ViewFlashcardsInStack(string stack)
    {
        var table = new Table();

        table.Title(stack);

        table.AddColumn("Id");
        table.AddColumn("Front");
        table.AddColumn("Back");

        table.AddRow("1", "Hasta la vista", "See you later");
        table.AddRow("2", "Adios", "Bye");

        AnsiConsole.Write(table);

        Console.WriteLine("\n\n");
        Console.WriteLine(new string('-', 20));
        Console.WriteLine("Input an ID of a flashcard, or 0 to exit: ");

        string? input;

        do
        {
            input = Console.ReadLine()?.Trim();
            if (input == "0")
            {
                StackController.CurrentStack(stack);
            }
        } while (string.IsNullOrEmpty(input));

        ViewOneFlashcard(input);
    }

    public static void ViewOneFlashcard(string Id){
        
    }


}

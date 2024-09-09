using Spectre.Console;

public class FlashcardController
{
    public static void ViewFlashcardsInStack(string stack)
    {
        Console.Clear();

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

        // input is the ID of the flashcard
        ViewOneFlashcard(input, stack);
    }

    public static void ViewOneFlashcard(string Id, string stack)
    {
        Console.Clear();

        var table = new Table();

        table.AddColumn("Front");

        table.AddRow("Adios");

                AnsiConsole.Write(table);


        Console.WriteLine("\n\nInput your answer to this card or 0 to exit");

        string? input;

        do
        {
            input = Console.ReadLine()?.Trim();
            if (input == "0")
            {
                ViewFlashcardsInStack(stack);
            }
        } while (string.IsNullOrEmpty(input));

        Console.WriteLine("Your answer was wrong");
    }
}

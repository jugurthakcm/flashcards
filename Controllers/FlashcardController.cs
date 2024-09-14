using Dapper;
using flashcards;
using Microsoft.Data.SqlClient;
using Models;
using Spectre.Console;

public class FlashcardController
{
    public static void ViewFlashcardsInStack(string stackName)
    {
        Console.Clear();

        var table = new Table();

        table.Title(stackName);

        table.AddColumn("Id");
        table.AddColumn("Front");
        table.AddColumn("Back");

        using (var connection = new SqlConnection(Variables.defaultConnection))
        {
            // Get the stack Id from DB using stack name
            var sql = $"SELECT * FROM Stacks where Name = '{stackName}'";
            var stack = connection.QuerySingleOrDefault(sql);

            if (stack != null)
            {
                // Get all the flashcards in the stack
                sql = $"SELECT * FROM Flashcards WHERE StackId =  {stack.Id}";
                connection.Execute(sql);

                IEnumerable<Flashcard> flashcards = connection.Query<Flashcard>(sql);

                // Loop thru the flashcards and insert them in table
                if (flashcards.Any())
                {
                    foreach (Flashcard f in flashcards)
                    {
                        table.AddRow(f.Id.ToString(), f.Front, f.Back);
                    }
                }
            }
        }

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
                StackController.CurrentStack(stackName);
            }
        } while (string.IsNullOrEmpty(input));

        // input is the ID of the flashcard
        ViewOneFlashcard(input, stackName);
    }

    public static void ViewOneFlashcard(string FlashcardId, string stackName)
    {
        Console.Clear();

        var table = new Table();

        table.AddColumn("Front");

        using (var connection = new SqlConnection(Variables.defaultConnection))
        {
            var sql = $"SELECT * FROM Flashcards where Id = {FlashcardId}";
            Flashcard flashcard = connection.QuerySingleOrDefault<Flashcard>(sql);

            if (flashcard != null)
            {
                table.AddRow(flashcard.Front);
            }

            AnsiConsole.Write(table);

            Console.WriteLine("\n\nInput your answer to this card or 0 to exit");

            string? input;

            // Ask user for answer of the back of the flashcard and check if answer is correct
            do
            {
                input = Console.ReadLine()?.Trim();
                if (input == "0")
                {
                    ViewFlashcardsInStack(stackName);
                }
                else if (input.ToLower() != flashcard?.Back.ToLower())
                    Console.WriteLine("Wrong answer, please try again!\n");
            } while (string.IsNullOrEmpty(input) || input.ToLower() != flashcard?.Back.ToLower());

            // If answer is correct then return to showing all cards
            Console.Write("\n\nYour answer is correct ! Press any key to continue...");
            Console.ReadLine();
            ViewFlashcardsInStack(stackName);
        }
    }

    public static void CreateFlashcardsInStack(string stackName)
    {
        Console.Clear();

        Console.Write("Input the front of the card: ");
        string front = Console.ReadLine();

        Console.Write("Input the back of the card: ");
        string back = Console.ReadLine();

        using (var connection = new SqlConnection(Variables.defaultConnection))
        {
            var sql = $"SELECT * FROM Stacks where Name = '{stackName}'";
            var stack = connection.QuerySingleOrDefault(sql);

            if (stack != null)
            {
                sql =
                    $"INSERT INTO Flashcards (Front, Back, StackId) VALUES ('{front}', '{back}',  '{stack.Id}')";
                connection.Execute(sql);
            }
        }

        StackController.CurrentStack(stackName);
    }
}

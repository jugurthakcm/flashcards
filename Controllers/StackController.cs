using Dapper;
using flashcards;
using Microsoft.Data.SqlClient;
using Models;
using Spectre.Console;

public class StackController
{
    public static void ShowStacks()
    {
        Console.Clear();

        var table = new Table();

        table.AddColumn("Name");

        // Use a loop to loop through the list of stacks
        using (var connection = new SqlConnection(Variables.defaultConnection))
        {
            var sql = "SELECT * FROM Stacks";
            IEnumerable<Stack> stacks = connection.Query<Stack>(sql);

            if (stacks.Any())
            {
                foreach (Stack s in stacks)
                {
                    table.AddRow(s.Name);
                }
            }
        }

        // Output table
        AnsiConsole.Write(table);

        Console.WriteLine("\n\nChoose a stack of flashcards to interact with, or input 0 to exit:");

        Console.WriteLine("If the name doesn't exit, a new stack will get created");

        string? input;

        do
        {
            input = Console.ReadLine()?.Trim();
            if (input == "0")
            {
                Menu.ShowMenu();
            }
        } while (string.IsNullOrEmpty(input));

        // Check if stack input exists or create one and selects it
        using (var connection = new SqlConnection(Variables.defaultConnection))
        {
            var sql = $"SELECT * FROM Stacks where Name = '{input}'";
            var stack = connection.QuerySingleOrDefault(sql);

            if (stack == null)
            {
                sql = $"INSERT INTO Stacks (Name) VALUES ('{input}')";
                connection.Execute(sql);
            }

            CurrentStack(input);
        }
    }

    public static void CurrentStack(string stack)
    {
        Console.Clear();
        Console.WriteLine(new string('-', 20));
        Console.WriteLine($"Current working stack: {stack}");

        Console.WriteLine("");
        Console.WriteLine("0 to return to main menu");
        Console.WriteLine("X to change current Stack");
        Console.WriteLine("V to view all Flashcards in stack");
        Console.WriteLine("A to view X amount of cards in stack");
        Console.WriteLine("C to create a flashcard in current stack");
        Console.WriteLine("E to edit a flashcard");
        Console.WriteLine("D to delete a flashcard");
        Console.WriteLine(new string('-', 20));

        Console.Write("\nWhat would you like to do? ");
        string? input = Console.ReadLine();

        switch (input)
        {
            case "0":
                Menu.ShowMenu();
                break;

            case "X":
                ShowStacks();
                break;

            case "V":
                FlashcardController.ViewFlashcardsInStack(stack);
                break;

            default:
                Console.WriteLine("Please choose an option from the menu");
                break;
        }
    }
}

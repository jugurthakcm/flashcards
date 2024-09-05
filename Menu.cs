public class Menu
{
    public static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Flash Cards");

        bool closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Manage Stacks");
            Console.WriteLine("2. Manage FlashCards");
            Console.WriteLine("3. Study");
            Console.WriteLine("4. View study session data");
            Console.WriteLine(new string('-', 20));

            Console.Write("\nWhat would you like to do? ");
            string? command = Console.ReadLine();

            switch (command)
            {
                case "0":
                    Console.WriteLine("\nGoodbye!\n");
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    StackController.ShowStacks();
                    break;
                case "2":
                    //await Controller.Insert();
                    break;
                case "3":
                    //Controller.Delete();
                    break;
                case "4":
                    //await Controller.Update();
                    break;

                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                    break;
            }
        }
    }
}

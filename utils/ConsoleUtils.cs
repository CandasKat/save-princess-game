using ConsoleTables;

class ConsoleUtils
{
    public static int PromptItems(List<IPromptable> items)
    {
        return ConsoleUtils.PromptItems(items, null, false, null);
    }

    public static int PromptItems(List<IPromptable> items, string? message, bool hasBackButton, string? context)
    {
        int minIndex = 1;
        if (message != null)
        {
            Console.WriteLine(message);
        }
        ConsoleTable table = new ConsoleTable();
        if (items[0].PromptHeader != null)
        {
            table.AddColumn(items[0].PromptHeader);
        }
        for (int i=0; i<items.Count; i++)
        {
            items[i].FormatPrompt(i + 1, table, context);
        }
        table.Write(Format.Alternative);
        if (hasBackButton)
        {
            minIndex = 0;
            Console.WriteLine("-------------------");
            Console.WriteLine("| 0 | Retour      |");
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }

        Console.Write("Choisissez une option entre " + minIndex + " et " + items.Count + " >> ");
        
        int index = -1;
        try
        {
            index = Int16.Parse(Console.ReadLine());
        }
        catch(Exception e) {}
        if (index >= 0 && index <= items.Count)
        {
            Console.Clear();
            return index-1;
        }

        Console.WriteLine("L'option choisie n'est pas valide. Choisissez une option entre " + minIndex + " et " + items.Count);
        return ConsoleUtils.PromptItems(items, message, hasBackButton, context);
    }

    public static void PressAnyKeyToContinue()
    {
        Console.WriteLine("<Appuyez sur n'importe quelle touche pour continuer>");
        Console.ReadKey();
    }
}
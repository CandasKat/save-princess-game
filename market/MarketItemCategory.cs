using ConsoleTables;

class MarketItemCategory : IPromptable
{
    public List<string> PromptHeader { get; } = new List<string>(){ "#", "Catégorie" };
    public string Name { get; set; }
    public List<MarketItem> Items { get; private set; } = new List<MarketItem>();

    public MarketItemCategory(string name)
    {
        Name = name;
    }

    public void FormatPrompt(int index, ConsoleTable table, string context)
    {
        table.AddRow(index, Name);
    }
}
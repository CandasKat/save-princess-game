using ConsoleTables;

class MenuOption : IPromptable
{
    public List<string> PromptHeader { get; } = new List<string>(){ "#", "" };
    public string Label;

    public MenuOption(string label)
    {
        Label = label;
    }
    public void FormatPrompt(int index, ConsoleTable table, string context)
    {
        table.AddRow(index, Label);
    }
}
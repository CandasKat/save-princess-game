using ConsoleTables;

abstract class HeroClass : IHeroModifier, IPromptable
{
    public List<string> PromptHeader { get; } = new List<string>(){ "#", "Nom", "Description" };
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract IWeapon BaseWeapon { get; }
    public virtual float HealthModifier { get => 1; }
    public virtual float SpeedModifier { get => 1; }
    public virtual float ForceModifier { get => 1; }
    public virtual float AgilityModifier { get => 1; }

    public void FormatPrompt(int index, ConsoleTable table, string context)
    {
        table.AddRow(index, Name, Description);
    }

}
using epsic_320_labo3;

class Equipment : IFixModifier, IBuyable
{
    public string Name  { get; private set; } 
    public string? Description  { get; private set; } 
    public int FixHealthModifier  { get; private set; } 
    public int FixSpeedModifier  { get; private set; } 
    public int FixForceModifier  { get; private set; } 
    public int FixAgilityModifier  { get; private set; } 

    public Equipment(
        string name,
        string description,
        int fixHealthModifier,
        int fixSpeedModifier,
        int fixForceModifier,
        int fixAgilityModifier
    )
    {
        Name = name;
        Description = description;
        FixHealthModifier = fixHealthModifier;
        FixSpeedModifier = fixSpeedModifier;
        FixForceModifier = fixForceModifier;
        FixAgilityModifier = fixAgilityModifier;
    }

    public IBuyable Clone()
    {
        return new Equipment(
            Name,
            Description,
            FixHealthModifier,
            FixSpeedModifier,
            FixForceModifier,
            FixAgilityModifier
        );
    }
}
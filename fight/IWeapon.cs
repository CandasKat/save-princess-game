using epsic_320_labo3;

interface IWeapon : IFixModifier, IPromptable
{
    public string Name { get; }
    public string Description { get; }
    public ICanFight? Owner { get; set; }
    public int MinDamage { get; }
    public int MaxDamage { get; }
    public int MaxUses { get; }
    public int Uses { get; }
    
    public string Use(FighterSlot target);
}
class Warrior : HeroClass
{
    public override string Name { get => "Guerrier"; }
    public override string Description { get; } = "PV -5%, force +30%, vitesse -5% et agilité -5%";
    public override float HealthModifier { get => 0.95f; }
    public override float SpeedModifier { get => 0.95f; }
    public override float ForceModifier { get => 1.3f; }
    public override float AgilityModifier { get => 0.95f; }
    public override Weapon BaseWeapon { get; } = new Weapon("Coup de poing", "Arme de base", 1, 2, 1000);

}
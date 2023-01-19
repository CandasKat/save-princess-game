class Voleur : HeroClass
{
    public override string Name { get => "Voleur"; }
    public override string Description { get; } = "PV -50%, force -20%, vitesse +60% et agilité +60%";
    public override float HealthModifier { get => 0.5f; }
    public override float SpeedModifier { get => 1.6f; }
    public override float ForceModifier { get => 0.8f; }
    public override float AgilityModifier { get => 1.6f; }
    public override Weapon BaseWeapon { get; } = new Weapon("Couteau de cuisine", "Arme de base", 1, 2, 1000);
}
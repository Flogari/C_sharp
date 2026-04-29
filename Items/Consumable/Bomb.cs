public class AttackUpgrade : Item, IConsumable
{
    public int AttackAmont { get; }

    public AttackUpgrade(string name, int atkamont) : base(name, $"Augmente l'attaque de {atkamont}")
    {
        AttackAmont = atkamont;
    }

    public void Use(Pokemon target)
    {
        Console.WriteLine($"Utilisation de {Name} sur {target.Name}...");
        target.Stats.BonusAttack = target.Stats.BonusAttack + AttackAmont;
        target.Stats.BonusSpecialAttack = target.Stats.BonusSpecialAttack + AttackAmont;
    }
}
public class Potion : Item, IConsumable
{
    public int HealAmount { get; }

    public Potion(string name, int healAmount) : base(name, $"Soigne {healAmount} PV.")
    {
        HealAmount = healAmount;
    }

    public void Use(Pokemon target)
    {
        Console.WriteLine($"Utilisation de {Name} sur {target.Name}...");
        target.Heal(HealAmount);
    }
}
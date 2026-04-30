public class LootEvent : GameEvent
{
    public int Floor;
    public LootEvent(int floor) : base("Coffre", "Un item vous attends !")
    {
        Floor = floor;
    }

    public override void Resolve(Pokemon target)
    {
        Random rng = new Random();
        int type = rng.Next();

        Console.ForegroundColor = ConsoleColor.Magenta;
        if (type == 0)
        {
            var w = new Weapon("Épée d'Acier", 2 + Floor);
            target.CurrentWeapon = w;
            Console.WriteLine($"[LOOT] {target.Name} équipe {w.Name} (+{w.AttackBonus} Atk) !");
        }
        else if (type == 1)
        {
            var a = new Armor("Plastron de Maille", 1 + Floor);
            target.CurrentArmor = a;
            Console.WriteLine($"[LOOT] {target.Name} équipe {a.Name} (+{a.DefenseBonus} Def) !");
        }
        else if (type == 2)
        {
            var p = new Potion("Super Potion", 15 + (Floor * 5));
            target.Item = p;
            Console.WriteLine($"[LOOT] {target.Name} ramasse {p.Name} ({p.HealAmount} PV) !");
        }
        else
        {
            var u = new AttackUpgrade("Parchemin d'attaque", 15 + (Floor * 5));
            target.Item = u;
            Console.WriteLine($"[LOOT] {target.Name} ramasse {u.Name} ({u.AttackAmont} atk) !");
        }
        Console.ResetColor();
    }
}
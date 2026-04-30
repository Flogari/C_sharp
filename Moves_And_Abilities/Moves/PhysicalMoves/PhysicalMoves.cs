public class PhysicalMove : Move
{
    public PhysicalMove(string name, int power, int accuracy, int range, PokemonType type) : base(name, power, accuracy, range, type)
    {
    }

    public override void Execute(Pokemon user, Pokemon target)
    {
        float multiplier = this.MoveType.GetMultiplier(target.PokeType.Element);

        float stab = 1.0f;

        if (user.PokeType.Element == this.MoveType.Element)
        {
            stab = 1.5f;
        }

        int baseDamage = (user.Stats.TotalAttack + Power) - (target.Stats.TotalDefense / 2);
        if (user.CurrentWeapon != null)
        {
            baseDamage += (int)(user.CurrentWeapon.AttackBonus);
        }
        if (target.CurrentArmor != null)
        {
            baseDamage -= (int)(target.CurrentArmor.DefenseBonus);
        }

        int finalDamage = (int)(baseDamage * multiplier * stab);

        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine($"{user.Name} utilise {Name} !");

        if (multiplier == 0.0f)
        {
            Console.WriteLine($"Ca n'affecte pas {target.Name}...");
        }
        else if (multiplier == 2.0f)
        {
            Console.WriteLine($"C'est super efficace ! ");
        }
        else if (multiplier == 0.5f)
        {
            Console.WriteLine($"Ce n'est pas très efficace...");
        }
        else
        {

        }
        Console.ResetColor();

        target.TakeDamage(finalDamage);
    }
}
public class HealMove : Move
{
    public HealMove(string name, int power, int accuracy, int range, PokemonType type) : base(name, power, accuracy, range, type)
    {
    }

    public override void Execute(Pokemon user, Pokemon target)
    {
        int heal = (user.Stats.TotalSpecialAttack + Power);

        Console.WriteLine($"{user.Name} utilise {Name} !");
        target.Heal(heal);
    }
}
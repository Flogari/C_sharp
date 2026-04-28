public class PhysicalMove : Move
{
    public PhysicalMove(string name, int power, int accuracy, PokemonType type) : base(name, power, accuracy, type)
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
    
    int finalDamage = (int)(baseDamage * multiplier * stab);

    Console.WriteLine($"{user.Name} utilise {Name} !");

    target.TakeDamage(finalDamage);
}
}
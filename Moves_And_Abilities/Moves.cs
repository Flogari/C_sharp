public abstract class Move
{
    public string Name { get; }
    public int Power { get; }
    public int Accuracy { get; }
    public PokemonType MoveType { get; }

    protected Move(string name, int power, int accuracy, PokemonType type)
    {
        Name = name;
        Power = power;
        Accuracy = accuracy;
        MoveType = type;
    }

    public abstract void Execute(Pokemon user, Pokemon target);
}
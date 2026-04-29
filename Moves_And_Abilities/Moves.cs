public abstract class Move
{
    public string Name { get; set; }
    public int Power { get; set; }
    public int Accuracy { get; set; }
    public int Range { get; set; }
    public PokemonType MoveType { get; }

    protected Move(string name, int power, int accuracy, int range, PokemonType type)
    {
        Name = name;
        Power = power;
        Accuracy = accuracy;
        Range = range;
        MoveType = type;
    }

    public abstract void Execute(Pokemon user, Pokemon target);
}
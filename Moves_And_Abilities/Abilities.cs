public abstract class Abilities
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Pokemon Owner { get; set; }

    public void Initialize(Pokemon owner)
    {
        Owner = owner;
        SubscribeEvents();
    }
    public abstract void SubscribeEvents();
}
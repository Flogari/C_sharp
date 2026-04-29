public abstract class GameEvent
{
    public string Title { get; protected set; }
    public string Description { get; protected set; }

    protected GameEvent(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public abstract void Resolve(Pokemon playerPokemon);
}
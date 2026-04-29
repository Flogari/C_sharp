public abstract class Equipment : Item
{
    protected Equipment(string name, string description) : base(name, description)
    {
        
    }

    public abstract void ApplyStats(Pokemon owner);
    public abstract void RemoveStats(Pokemon owner);
}
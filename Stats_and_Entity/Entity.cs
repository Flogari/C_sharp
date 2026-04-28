public abstract class Entity
{
    public string Name { get; set; }
    
    public Entity(string name)
    {
        Name = name;
    }
}
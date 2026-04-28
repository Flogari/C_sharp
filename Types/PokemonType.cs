public enum ElementType
{
    Bug,
    Dark,
    Dragon,
    Electric,
    Fairy,
    Fighting,
    Fire,
    Flying,
    Ghost,
    Grass,
    Ground,
    Ice,
    Normal,
    Poison,
    Psychic,
    Rock,
    Steel,
    Water

}

public abstract class PokemonType
{
    public ElementType Element { get; set; }
    public string Name => Element.ToString();

    // Chaque type dira s'il est fort, faible ou neutre
    public abstract float GetMultiplier(ElementType targetElement);
}
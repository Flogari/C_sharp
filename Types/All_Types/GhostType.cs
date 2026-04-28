public class GhostType : PokemonType
{
    public GhostType() => Element = ElementType.Ghost;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Ghost => 2.0f,
            ElementType.Psychic => 2.0f,

            ElementType.Dark => 0.5f,

            ElementType.Normal => 0.0f,

            _ => 1.0f
        };
    }
}
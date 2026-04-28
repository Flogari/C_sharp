public class DarkType : PokemonType
{
    public DarkType() => Element = ElementType.Dark;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Ghost => 2.0f,
            ElementType.Psychic => 2.0f,

            ElementType.Dark => 0.5f,
            ElementType.Fairy => 0.5f,
            ElementType.Fighting => 0.5f,

            _ => 1.0f
        };
    }
}
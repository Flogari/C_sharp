public class BugType : PokemonType
{
    public BugType() => Element = ElementType.Bug;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Dark => 2.0f,
            ElementType.Grass => 2.0f,
            ElementType.Psychic => 2.0f,

            ElementType.Fairy => 0.5f,
            ElementType.Fire => 0.5f,
            ElementType.Fighting => 0.5f,
            ElementType.Flying => 0.5f,
            ElementType.Ghost => 0.5f,
            ElementType.Poison => 0.5f,
            ElementType.Steel => 0.5f,

            _ => 1.0f
        };
    }
}
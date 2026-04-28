public class PsychicType : PokemonType
{
    public PsychicType() => Element = ElementType.Psychic;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Fighting => 2.0f,
            ElementType.Poison => 2.0f,

            ElementType.Psychic => 0.5f,
            ElementType.Steel => 0.5f,

            ElementType.Dark => 0.0f,

            _ => 1.0f
        };
    }
}
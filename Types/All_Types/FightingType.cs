public class FightingType : PokemonType
{
    public FightingType() => Element = ElementType.Fighting;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Dark => 2.0f,
            ElementType.Ice => 2.0f,
            ElementType.Normal => 2.0f,
            ElementType.Rock => 2.0f,
            ElementType.Steel => 2.0f,

            ElementType.Bug => 0.5f,
            ElementType.Fairy => 0.5f,
            ElementType.Flying => 0.5f,
            ElementType.Psychic => 0.5f,
            ElementType.Poison => 0.5f,

            ElementType.Ghost => 0.0f,

            _ => 1.0f
        };
    }
}
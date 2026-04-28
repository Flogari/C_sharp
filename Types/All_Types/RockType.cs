public class RockType : PokemonType
{
    public RockType() => Element = ElementType.Rock;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {

            ElementType.Bug => 2.0f,
            ElementType.Fire => 2.0f,
            ElementType.Flying => 2.0f,
            ElementType.Grass => 2.0f,

            ElementType.Fighting => 0.5f,
            ElementType.Ground => 0.5f,
            ElementType.Steel => 0.5f,

            _ => 1.0f
        };
    }
}
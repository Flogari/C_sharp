public class FlyingType : PokemonType
{
    public FlyingType() => Element = ElementType.Flying;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {
            ElementType.Bug => 2.0f,
            ElementType.Fighting => 2.0f,
            ElementType.Grass => 2.0f,

            ElementType.Electric => 0.5f,
            ElementType.Rock => 0.5f,
            ElementType.Steel => 0.5f,

            _ => 1.0f
        };
    }
}
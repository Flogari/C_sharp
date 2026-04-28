public class NormalType : PokemonType
{
    public NormalType() => Element = ElementType.Normal;

    public override float GetMultiplier(ElementType targetElement)
    {
        return targetElement switch
        {

            ElementType.Rock => 0.5f,
            ElementType.Steel => 0.5f,

            ElementType.Ghost => 0.0f,

            _ => 1.0f
        };
    }
}